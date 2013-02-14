using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
    class RepeatWarmerTest
    {
		private const string Url = "http://www.google.com/";

		[Test]
		public void RepeatWarmer_CanBeInitialised()
		{
			var warmer = new RepeatWarmer(null, null, null, 1);

			Assert.IsInstanceOf<RepeatWarmer>(warmer);
		}

		[Test]
		public void Warm_WhenInitialCallFails_ThenRetry()
		{

			var config = BaseConfig();
			var requester = BaseRequester();
			var logger = BaseLogger();

			var warmer = new RepeatWarmer(config.Object, requester.Object, logger.Object, 2);

			var results = warmer.Warm();

			Assert.AreEqual(1, results.Count);

			Assert.AreEqual(Check.Ok, results[0].Status);
		}

		[Test]
		public void Warm_WhenInitialCallFails_ThenRetryOnlyTheNumberOfTimesWeAreConfiguredFor()
		{

			var config = BaseConfig();
			var requester = BaseRequester();
			var logger = BaseLogger();

			var warmer = new RepeatWarmer(config.Object, requester.Object, logger.Object, 1);

			var results = warmer.Warm();

			Assert.AreEqual(1, results.Count);

			Assert.AreNotEqual(Check.Ok, results[0].Status);
		}

		private static Mock<IConfig> BaseConfig()
		{
			var config = new Mock<IConfig>();
			config.Setup(m => m.Checks).Returns(new List<Check>
				{
					new Check{Url = Url}
				});

			return config;
		}

		private static Mock<IRequester> BaseRequester()
		{
			var requester = new Mock<IRequester>();
			var statues = new List<int> { 500, Check.Ok };

			var count = 0;

			requester.Setup(m => m.Check(It.IsAny<Check>()))
				.Callback((Check c) => c.Status = statues[count++]);

			return requester;
		}

		private static Mock<ILogger> BaseLogger()
		{
			var logger = new Mock<ILogger>();
			logger.Setup(m => m.Log(It.IsAny<Check>()));
			logger.Setup(m => m.Close());

			return logger;
		}
    }
}
