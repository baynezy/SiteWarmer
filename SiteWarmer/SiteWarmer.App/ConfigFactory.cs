using SiteWarmer.Core.Config;

namespace SiteWarmer.App
{
	public class ConfigFactory
	{
		public static IConfig Create(Options options)
		{
			IConfig config;

			if (IsXmlFile(options.Inputfile))
			{
				config = new XmlConfig(options.Inputfile);
			}
			else
			{
				config = new FileConfig(options.Inputfile);
			}

			return config;
		}

		private static bool IsXmlFile(string inputfile)
		{
			return inputfile.EndsWith(".xml");
		}
	}
}
