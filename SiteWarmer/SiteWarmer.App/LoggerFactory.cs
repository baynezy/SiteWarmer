﻿using SiteWarmer.Core.Logging;

namespace SiteWarmer.App
{
	public class LoggerFactory
	{
		public static ILogger CreateLogger(Options options)
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
	}
}
