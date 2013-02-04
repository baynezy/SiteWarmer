using System;
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
				var config = new FileConfig(options.Inputfile);
				var requester = new Requester();
				var logger = LoggerFactory.Create(options);

				var warmer = WarmerFactory.Create(options, config, requester, logger);

				warmer.Warm();
			}
			else
			{
				Environment.Exit(1);
			}
		}
	}
}
