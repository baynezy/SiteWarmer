using System;
using System.Collections;
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

			var mockRequester = new Mock<IRequester>();
			mockRequester.Setup(m => m.Check(It.IsAny<Check>()));

			var mockLogger = new Mock<ILogger>();
			mockLogger.Setup(m => m.Log(It.IsAny<Check>()));

			var warmer = new CustomWarmer(mockConfig.Object, mockRequester.Object);

			var logger = mockLogger.Object;

			warmer.Warm(logger.Log);

			mockRequester.Verify(f => f.Check(It.IsAny<Check>()), Times.Exactly(2));
			mockLogger.Verify(f => f.Log(It.IsAny<Check>()), Times.Exactly(2));
		}
	}
}
