using System.Collections.Generic;
using System.Linq;

namespace SiteWarmer.Core.Config
{
	public class ManualConfig : IConfig
	{
		public ManualConfig()
		{
			Checks = new List<Check>();
		}
		
		public ManualConfig(IEnumerable<string> urls)
		{
			var select = from input in urls
			             select new Check
				             {
					             Url = input
				             };
			Checks = select.ToList();
		}

		public ManualConfig(IList<Check> urls)
		{
			Checks = urls;
		}

		public IList<Check> Checks { get; private set; }

		public void Add(Check check)
		{
			Checks.Add(check);
		}
	}
}
