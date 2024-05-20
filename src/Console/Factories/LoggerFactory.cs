using SiteWarmer.Core.Logging;

namespace Console.Factories;

public static class LoggerFactory
{
    public static ILogger Create(bool? logError)
    {
        ILogger logger;

        if (logError ?? false)
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