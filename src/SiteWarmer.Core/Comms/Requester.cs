using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms;

/// <summary>
/// Makes HTTP requests to URLs to see if they are responding correctly.
/// </summary>
public class Requester : IRequester
{
    private readonly HttpClient _httpClient;

    public Requester()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
        _httpClient.DefaultRequestHeaders.ConnectionClose = true;
    }
    public async Task CheckAsync(Check check)
    {
        await RunCheckAsync(check);
    }

    private async Task RunCheckAsync(Check check)
    {
        var request = await _httpClient.GetAsync(check.Url);
        var response = await request.Content.ReadAsStringAsync();
        check.Status = (int)request.StatusCode;
        check.Source = response;
    }
}