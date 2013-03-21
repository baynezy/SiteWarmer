using System.Linq;
using SiteWarmer.Core.Config;

namespace SiteWarmer.App.Factories
{
	public class ConfigFactory
	{
		public static IConfig Create(Options options)
		{
			IConfig config;

			if (HasMultipleFiles(options))
			{
				var configCollection = new ConfigCollection();

				foreach (var file in options.Inputfiles)
				{
					configCollection.Add(GetConfig(file));
				}

				config = configCollection;
			}
			else
			{
				config = GetConfig(options.Inputfiles.First());
			}

			return config;
		}

		private static bool HasMultipleFiles(Options options)
		{
			return options.Inputfiles.Count > 1;
		}

		private static IConfig GetConfig(string file)
		{
			IConfig config;
			
			if (IsXmlFile(file))
			{
				config = new XmlConfig(file);
			}
			else
			{
				config = new FileConfig(file);
			}

			return config;
		}

		private static bool IsXmlFile(string inputfile)
		{
			return inputfile.EndsWith(".xml");
		}
	}
}
