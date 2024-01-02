using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core;

/// <summary>
/// Warms Urls and logs the results. Repeats any failed checks as many times as you indicate
/// </summary>
public class RepeatWarmer : Warmer
{
    private readonly int _timesToRepeat;
    private int _numberOfRuns;

    /// <summary>
    /// Instantiate new RepeatWarmer
    /// </summary>
    /// <param name="config">Configuration of Check</param>
    /// <param name="requester">HTTP requester</param>
    /// <param name="logger">Logging strategy for Warmer</param>
    /// <param name="timesToRepeat">How many times the Warmer will retry on a failure</param>
    public RepeatWarmer(IConfig config, IRequester requester, ILogger logger, int timesToRepeat) : base(config, requester, logger)
    {
        _timesToRepeat = timesToRepeat;
        _numberOfRuns = 0;
    }

    protected override async Task<bool> RunChecksAsync(IList<Check> checks)
    {
        await base.RunChecksAsync(checks);

        _numberOfRuns++;

        if (CompletedAllRuns())
        {
            return true;
        }

        return !StillHasErrors(checks) || await RunChecksAsync(OnlyErrors(checks));
    }

    private bool CompletedAllRuns()
    {
        return _numberOfRuns >= _timesToRepeat;
    }

    private static bool StillHasErrors(IEnumerable<Check> checks)
    {
        return OnlyErrors(checks).Count() != 0;
    }

    private static IList<Check> OnlyErrors(IEnumerable<Check> checks)
    {
        return checks.Where(c => !c.Passed()).ToList();
    }
}