using System.Collections.Generic;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core
{
	/// <summary>
	/// Warms Urls and logs the results
	/// </summary>
	public class Warmer
	{
		private readonly IConfig _config;
		private readonly CustomWarmer _warmer;
		private readonly ILogger _logger;

		public Warmer(IConfig config, IRequester requester, ILogger logger)
		{
			_config = config;
			_warmer = new CustomWarmer(config, requester);
			_logger = logger;
		}

		public IList<Check> Warm()
		{
			var checks = _config.Checks;

			RunChecks(checks);

			_logger.Close();

			return checks;
		}

		protected virtual bool RunChecks(IList<Check> checks)
		{
			_warmer.Warm(_logger.Log);

			return true;
		}
	}
}
