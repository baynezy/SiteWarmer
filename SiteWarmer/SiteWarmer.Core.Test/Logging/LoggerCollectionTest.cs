using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test.Logging
{
	[TestFixture]
	class LoggerCollectionTest
	{
		[Test]
		public void Size_EmptyCollection_ReturnsZeroSize()
		{
			const int expectedSize = 0;
			var collection = new LoggerCollection();

			Assert.AreEqual(expectedSize, collection.Size());
		}

		[Test]
		public void Add_AddingLoggerToCollection_ReturnsCorrectSize()
		{
			const int expectedSize = 1;
			var collection = new LoggerCollection();
			var logger = new ConsoleLogger();

			collection.Add(logger);

			Assert.AreEqual(expectedSize, collection.Size());
		}

		[Test]
		public void LoggerCollection_ImplementsILogger()
		{
			var collection = new LoggerCollection();

			Assert.IsInstanceOf(typeof(ILogger), collection);
		}

		[Test]
		public void Log_CallingLog_CallsInternalLoggersLogMethodTheCorrectNumberOfTimes()
		{
			var collection = new LoggerCollection();
			var logger1 = new Mock<ILogger>();
			var logger2 = new Mock<ILogger>();

			logger1.Setup(m => m.Log(It.IsAny<Check>()));
			logger2.Setup(m => m.Log(It.IsAny<Check>()));

			collection.Add(logger1.Object);
			collection.Add(logger2.Object);


			collection.Log(new Check
			               	{
			               		Url = "http://www.google.com/",
								Status = 200
			               	});

			logger1.Verify(f => f.Log(It.IsAny<Check>()), Times.Once());
			logger2.Verify(f => f.Log(It.IsAny<Check>()), Times.Once());
		}
	}
}
