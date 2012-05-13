using System.Collections.Generic;

namespace SiteWarmer.Core
{
	public class Warmer
	{
		private readonly IConfig _config;
		private readonly Requester _requester;

		public Warmer(IConfig config, Requester requester)
		{
			_config = config;
			_requester = requester;
		}

		public List<Check> Warm()
		{
			var checks = _config.Checks;

			foreach (var check in checks)
			{
				_requester.Check(check);
			}

			return checks;
		}
	}
}
