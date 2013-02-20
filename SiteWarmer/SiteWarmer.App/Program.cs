using System;
using System.Xml;
using CommandLine;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;

namespace SiteWarmer.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();
			var parser = new CommandLineParser();

			if (parser.ParseArguments(args, options, Console.Error))
			{
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
	}
}
