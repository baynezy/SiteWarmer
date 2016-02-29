using System.Collections.Generic;
using System.Linq;
using SiteWarmer.Core.Collection;

namespace SiteWarmer.Core.Config
{
	/// <summary>
	/// A combined collection of config files. These get combined into one list that will get executed.
	/// </summary>
	public class ConfigCollection : AbstractCollection<IConfig>, IConfig
	{
		/// <summary>
		/// The collection of Checks contained within the config
		/// </summary>
		public IList<Check> Checks
		{
			get { return CombineChecks(); }
		}

		private IList<Check> CombineChecks()
		{
			var checks = new List<Check>();

			return Items.Aggregate(checks, (current, config) => current.Concat(config.Checks).ToList());
		}
	}
}
