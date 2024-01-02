using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms;

/// <summary>
/// Can check URLs to see if they are responding as expected
/// </summary>
public interface IRequester
{
    /// <summary>
    /// Requests the Url contained within the Check and updates the Check with the 
    /// StatusCode and HTML Source of the URL
    /// </summary>
    /// <param name="check">The Check we wish to request</param>
    Task CheckAsync(Check check);
}