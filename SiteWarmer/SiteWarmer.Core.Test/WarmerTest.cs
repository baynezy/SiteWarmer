using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class WarmerTest
	{
		private Warmer _warmer;

		[SetUp]
		public void Setup()
		{
			var config = new FileConfig(@"..\..\Data\urls.txt");
			var requester = new Requester();
			var logger = new ConsoleLogger();
			_warmer = new Warmer(config, requester, logger);
		}

		[Test]
		public void Warm()
		{
			var checks = _warmer.Warm();

			Assert.AreEqual(4, checks.Count);
			Assert.AreEqual(500, checks[0].Status);
			Assert.AreEqual(200, checks[1].Status);
			Assert.AreEqual(200, checks[2].Status);
			Assert.AreEqual(404, checks[3].Status);
		}

		[Test]
		public void WarmClosesLogger()
		{
			var mockLogger = new Mock<ILogger>();
			mockLogger.Setup(m => m.Log(It.IsAny<Check>()));
			mockLogger.Setup(m => m.Close());
			var mockRequester = new Mock<IRequester>();
			mockRequester.Setup(m => m.Check(It.IsAny<Check>()));
			var mockConfig = new Mock<IConfig>();
			mockConfig.SetupGet(m => m.Checks).Returns(new List<Check>
			                                           	{
			                                           		new Check
			                                           			{
			                                           				Status = 200,
																	Url = "http://www.google.com"
			                                           			}
			                                           	});


			var warmer = new Warmer(mockConfig.Object, mockRequester.Object, mockLogger.Object);
			warmer.Warm();

			mockLogger.Verify(f => f.Close(), Times.Once());
		}
	}
}
