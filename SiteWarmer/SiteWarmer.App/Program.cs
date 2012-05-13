using System;
using System.Collections.Generic;
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
			var warmer = new Warmer(config, requester);

			var checks = warmer.Warm();

			Log(checks);
		}

		private static void Log(IEnumerable<Check> checks)
		{
			foreach (var check in checks)
			{
				Console.WriteLine(String.Format("{0}: {1}", check.Status, check.Url));
			}
		}
	}
}
