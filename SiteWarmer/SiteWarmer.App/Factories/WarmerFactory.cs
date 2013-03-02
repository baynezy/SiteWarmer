using SiteWarmer.Core;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.App.Factories
{
	public class WarmerFactory
	{
		public static Warmer Create(Options options, IConfig config, IRequester requester, ILogger logger)
		{
			return options.Retries > 1 ? new RepeatWarmer(config, requester, logger, options.Retries) : new Warmer(config, requester, logger);
		}
	}
}
