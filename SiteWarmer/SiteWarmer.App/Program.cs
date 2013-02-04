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
				var logger = CreateLogger(options);

				var warmer = CreateWarmer(config, requester, logger, options);

				warmer.Warm();
			}
			else
			{
				Environment.Exit(1);
			}
		}

		private static ILogger CreateLogger(Options options)
		{
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

			return logger;
		}

		private static Warmer CreateWarmer(IConfig config, IRequester requester, ILogger logger, Options options)
		{
			return options.Retries > 1 ? new RepeatWarmer(config, requester, logger, options.Retries) : new Warmer(config, requester, logger);
		}
	}
}
