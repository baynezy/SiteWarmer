using System.Collections.Generic;
using System.Linq;

namespace SiteWarmer.Core.Config
{
	/// <summary>
	/// A maunally created config. This allows programatical configuration of the configuration.
	/// </summary>
	public class ManualConfig : IConfig
	{
		/// <summary>
		/// Create empty configuration
		/// </summary>
		public ManualConfig()
		{
			Checks = new List<Check>();
		}
		
		/// <summary>
		/// Initialise a config with a collection of URLs
		/// </summary>
		/// <param name="urls">URLs to initialise the configuration with</param>
		public ManualConfig(IEnumerable<string> urls)
		{
			var select = from input in urls
			             select new Check
				             {
					             Url = input
				             };
			Checks = select.ToList();
		}

		/// <summary>
		/// Initialise a config with a collection of URLs
		/// </summary>
		/// <param name="urls">URLs to initialise the configuration with</param>
		public ManualConfig(IList<Check> urls)
		{
			Checks = urls;
		}

		/// <summary>
		/// The collection of Checks contained within the config
		/// </summary>
		public IList<Check> Checks { get; private set; }

		/// <summary>
		/// Add a new check to the end of the configuration
		/// </summary>
		/// <param name="check">The check to add to the configuration</param>
		public void Add(Check check)
		{
			Checks.Add(check);
		}
	}
}
