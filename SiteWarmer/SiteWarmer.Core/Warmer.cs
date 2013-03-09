using System.Collections.Generic;
using System.Threading.Tasks;
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
		private readonly IRequester _requester;
		private readonly ILogger _logger;

		public Warmer(IConfig config, IRequester requester, ILogger logger)
		{
			_config = config;
			_requester = requester;
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
			Parallel.ForEach(checks, check =>
			{
				_requester.Check(check);
				_logger.Log(check);
			});

			return true;
		}
	}
}
