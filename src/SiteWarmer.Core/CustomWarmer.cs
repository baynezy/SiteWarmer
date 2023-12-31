using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core;

/// <summary>
/// Event based Warmer, allowing more flexibility than other Warmer implementations
/// </summary>
public class CustomWarmer
{
    private readonly IConfig _config;
    private readonly IRequester _requester;

    /// <summary>
    /// Instantiate CustomerWarmer
    /// </summary>
    /// <param name="config">Configuration of Check</param>
    /// <param name="requester">HTTP requester</param>
    public CustomWarmer(IConfig config, IRequester requester)
    {
        _config = config;
        _requester = requester;
    }

    /// <summary>
    /// Warm all URLs in IConfig
    /// </summary>
    /// <param name="action">Custom action to execute after each Check is requested</param>
    public async Task WarmAsync(Action<Check> action)
    {
        await WarmAsync(_config.Checks, action);
    }

    /// <summary>
    /// Warm all URLs in the Check collection passed in
    /// </summary>
    /// <param name="checks">Check collection to warm</param>
    /// <param name="action">Custom action to execute after each Check is requested</param>
    public async Task WarmAsync(IEnumerable<Check> checks, Action<Check> action)
    {
        var tasks = checks.Select(async check =>
        {
            await _requester.CheckAsync(check);
            action.Invoke(check);
        });
        await Task.WhenAll(tasks);
    }
}