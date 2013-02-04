using System;
using CommandLine;
using SiteWarmer.Core;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

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
				ILogger logger;

				if (options.LogError)
				{
					var collection = new LoggerCollection();
					collection.Add(new ConsoleLogger());
					collection.Add(new FileLogger());

					logger = collection;
				}
				else
				{
					logger = new ConsoleLogger();
				}

				Warmer warmer;

				if (options.Retries > 1)
				{
					warmer = new RepeatWarmer(config, requester, logger, options.Retries);
				}
				else
				{
					warmer = new Warmer(config, requester, logger);
				}

				warmer.Warm();
			}
			else
			{
				Environment.Exit(1);
			}
		}
	}
}
