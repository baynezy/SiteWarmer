using SiteWarmer.Core;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace Console.Factories;

public static class WarmerFactory
{
    public static Warmer Create(int? retries, IConfig config, IRequester requester, ILogger logger)
    {
        return retries > 1 ? new RepeatWarmer(config, requester, logger, (int) retries) : new Warmer(config, requester, logger);
    }
}