using System;
using System.Xml;
using CommandLine;
using SiteWarmer.App.Factories;
using SiteWarmer.Core.Comms;

namespace SiteWarmer.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();
			var parser = new Parser();

			if (parser.ParseArguments(args, options))
			{
				if (ShouldShowVersion(options))
				{
					OutputVersion();
					return;
				}

				try
				{
					var config = ConfigFactory.Create(options);
					var requester = new Requester();
					var logger = LoggerFactory.Create(options);

					var warmer = WarmerFactory.Create(options, config, requester, logger);

					warmer.Warm();
				}
				catch (XmlException e)
				{
					Console.WriteLine("");
					Console.WriteLine("Invalid XML Config File");
					Console.WriteLine("-----------------------");
					Console.WriteLine(e.Message);
				}
			}
			else
			{
				Environment.Exit(1);
			}
		}

		private static bool ShouldShowVersion(Options options)
		{
			return options.Version;
		}

		private static void OutputVersion()
		{
			Console.WriteLine("SiteWarmer - Version " + CalculateVersion());
		}

		private static string CalculateVersion()
		{
			return Version.GetAppVersion();
		}
	}
}
