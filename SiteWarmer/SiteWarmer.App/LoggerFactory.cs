using SiteWarmer.Core.Logging;

namespace SiteWarmer.App
{
	public class LoggerFactory
	{
		public static ILogger Create(Options options)
		{
			ILogger logger;

			if (options.LogError)
			{
				var collection = new LoggerCollection();
				collection.Add(new ConsoleLogger());
				collection.Add(new FileLogger(new FileHelper()));

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
