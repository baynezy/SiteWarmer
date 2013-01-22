using System;
using CommandLine;
using SiteWarmer.Core;

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
				var warmer = new Warmer(config, requester, logger);

				warmer.Warm();
			}
			else
			{
				Environment.Exit(1);
			}
		}
	}
}
