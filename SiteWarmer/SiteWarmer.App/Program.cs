using System;
using SiteWarmer.Core;

namespace SiteWarmer.App
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("You must specify a configuration file for the application");
				return;
			}

			var config = new FileConfig(args[0]);
			var requester = new Requester();
			var logger = new ConsoleLogger();
			var warmer = new Warmer(config, requester, logger);

			warmer.Warm();
		}
	}
}
