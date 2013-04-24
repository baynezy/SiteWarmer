using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class CustomWarmerTest
	{
		[Test]
		public void Warm_WhenProvidingDelegate_ThenRunDelegate()
		{
			var mockConfig = MockConfig();
			var mockRequester = MockRequester();
			var mockLogger = MockLogger();
			var warmer = new CustomWarmer(mockConfig.Object, mockRequester.Object);
			var logger = mockLogger.Object;

			warmer.Warm(logger.Log);

			mockRequester.Verify(f => f.Check(It.IsAny<Check>()), Times.Exactly(2));
			mockLogger.Verify(f => f.Log(It.IsAny<Check>()), Times.Exactly(2));
		}

		[Test]
		public void Warm_WhenProvidingMultiCallDelegate_ThenRunDelegate()
		{
			var mockConfig = MockConfig();
			var mockRequester = MockRequester();
			var mockLogger = MockLogger();
			var warmer = new CustomWarmer(mockConfig.Object, mockRequester.Object);
			var logger = mockLogger.Object;

			var tracker = 0;

			warmer.Warm(
				delegate(Check check)
					{
						logger.Log(check);
						tracker++;
					}
				);

			mockRequester.Verify(f => f.Check(It.IsAny<Check>()), Times.Exactly(2));
			mockLogger.Verify(f => f.Log(It.IsAny<Check>()), Times.Exactly(2));
			Assert.That(tracker, Is.EqualTo(2));
		}

		private static Mock<ILogger> MockLogger()
		{
			var mockLogger = new Mock<ILogger>();
			mockLogger.Setup(m => m.Log(It.IsAny<Check>()));
			return mockLogger;
		}

		private static Mock<IRequester> MockRequester()
		{
			var mockRequester = new Mock<IRequester>();
			mockRequester.Setup(m => m.Check(It.IsAny<Check>()));
			return mockRequester;
		}

		private static Mock<IConfig> MockConfig()
		{
			var mockConfig = new Mock<IConfig>();
			mockConfig.Setup(m => m.Checks).Returns(
				new List<Check>
					{
						new Check
							{
								Url = "http://www.google.co.uk/"
							},
						new Check
							{
								Url = "http://www.github.com/"
							}
					});
			return mockConfig;
		}
	}
}
