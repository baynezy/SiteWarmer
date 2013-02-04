using System.Collections.Generic;
using System.Linq;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core
{
	public class RepeatWarmer : Warmer
	{
		private readonly int _timesToRepeat;
		private int _numberOfRuns;

		public RepeatWarmer(IConfig config, IRequester requester, ILogger logger, int timesToRepeat) : base(config, requester, logger)
		{
			_timesToRepeat = timesToRepeat;
			_numberOfRuns = 0;
		}

		protected override bool RunChecks(IList<Check> checks)
		{
			base.RunChecks(checks);

			_numberOfRuns++;

			if (CompletedAllRuns())
			{
				return true;
			}

			return !StillHasErrors(checks) || RunChecks(OnlyErrors(checks));
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
			return checks.Where(c => c.Status != Check.Ok).ToList();
		}
	}
}
