using System;
using System.Threading.Tasks;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core
{
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
		/// <param name="config">Configuraton of Check</param>
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
		public void Warm(Action<Check> action)
		{
			Parallel.ForEach(_config.Checks, check =>
				{
					_requester.Check(check);
					action.Invoke(check);
				});
		}
	}
}
