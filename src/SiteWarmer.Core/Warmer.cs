using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core;

/// <summary>
/// Warms Urls and logs the results
/// </summary>
public class Warmer
{
    private readonly IConfig _config;
    private readonly CustomWarmer _warmer;
    private readonly ILogger _logger;

    /// <summary>
    /// Instantiate new Warmer
    /// </summary>
    /// <param name="config">Configuration of Check</param>
    /// <param name="requester">HTTP requester</param>
    /// <param name="logger">Logging strategy for Warmer</param>
    public Warmer(IConfig config, IRequester requester, ILogger logger)
    {
        _config = config;
        _warmer = new CustomWarmer(config, requester);
        _logger = logger;
    }

    /// <summary>
    /// Warm URLs from IConfig
    /// </summary>
    /// <returns>Final status of Check collection on completion</returns>
    public async Task<IList<Check>> WarmAsync()
    {
        var checks = _config.Checks;

        await RunChecksAsync(checks);

        _logger.Close();

        return checks;
    }

    /// <summary>
    /// Specific action when Check collections are requested
    /// </summary>
    /// <param name="checks">Collection of Check to run</param>
    /// <returns>Whether all checks have completed</returns>
    protected virtual async Task<bool> RunChecksAsync(IList<Check> checks)
    {
        await _warmer.WarmAsync(checks, _logger.Log);

        return true;
    }
}