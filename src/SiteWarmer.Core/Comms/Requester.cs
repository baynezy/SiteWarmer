using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms;

/// <summary>
/// Makes HTTP requests to URLs to see if they are responding correctly.
/// </summary>
public class Requester : IRequester
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Create a new instance of the Requester class. This will create a new HttpClient with a timeout of 10 seconds and set the Connection header to close after each request.
    /// </summary>
    public Requester() : this(new HttpClient())
    {
    }

    /// <summary>
    /// Create a new instance of the Requester class with a custom HttpClient. This will set the timeout of the HttpClient to 10 seconds and set the Connection header to close after each request.
    /// </summary>
    /// <param name="httpClient">The HttpClient to use for making requests. This allows you to customize the HttpClient with things like proxy settings, custom headers, etc.</param>
    public Requester(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
        _httpClient.DefaultRequestHeaders.ConnectionClose = true;
    }

    /// <summary>
    /// Make an HTTP request to the URL specified in the Check object and update the Check object with the response status code and response body.
    /// </summary>
    /// <param name="check">The Check object containing the URL to check and where to store the response status code and response body.</param>
    public async Task CheckAsync(Check check)
    {
        await RunCheckAsync(check);
    }

    private async Task RunCheckAsync(Check check)
    {
        var request = await _httpClient.GetAsync(check.Url);
        var response = await request.Content.ReadAsStringAsync();
        check.Status = (int) request.StatusCode;
        check.Source = response;
    }
}