using NUnit.Framework;
using SiteWarmer.App.Factories;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.App.Test.Factories
{
	[TestFixture]
	class LoggerFactoryTest
	{
		[Test]
		public void Create_WhenLoggingErrors_ShoudReturnLoggerCollection()
		{
			var options = new Options
				{
					LogError = true
				};
			
			var logger = LoggerFactory.Create(options);

			Assert.That(logger, Is.InstanceOf<LoggerCollection>());

			var collection = (LoggerCollection)logger;

			Assert.That(collection.Size(), Is.EqualTo(2));
		}

		[Test]
		public void Create_WhenNotLoggingErrors_ShoudReturnConsoleLogger()
		{
			var options = new Options
			{
				LogError = false
			};

			var logger = LoggerFactory.Create(options);

			Assert.That(logger, Is.InstanceOf<ConsoleLogger>());
		}
	}
}
