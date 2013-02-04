using NUnit.Framework;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.App.Test
{
	[TestFixture]
	class LoggerFactoryTest
	{
		[Test]
		public void CreateLogger_WhenLoggingErrors_ShoudReturnLoggerCollection()
		{
			var options = new Options
				{
					LogError = true
				};
			
			var logger = LoggerFactory.CreateLogger(options);

			Assert.IsInstanceOf(typeof(LoggerCollection), logger);

			var collection = (LoggerCollection)logger;

			Assert.AreEqual(2, collection.Size());
		}

		[Test]
		public void CreateLogger_WhenNotLoggingErrors_ShoudReturnConsoleLogger()
		{
			var options = new Options
			{
				LogError = false
			};

			var logger = LoggerFactory.CreateLogger(options);

			Assert.IsInstanceOf(typeof(ConsoleLogger), logger);
		}
	}
}
