using Moq;
using NUnit.Framework;
using SiteWarmer.Core;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.App.Test
{
	[TestFixture]
	class WarmerFactoryTest
	{
		[Test]
		public void Create_WhenRetriesAreZero_ReturnWarmer()
		{
			var options = new Options
				{
					Retries = 0
				};

			var config = new Mock<IConfig>();
			var requester = new Mock<IRequester>();
			var logger = new Mock<ILogger>();

			var warmer = WarmerFactory.Create(options, config.Object, requester.Object, logger.Object);

			Assert.That(warmer, Is.InstanceOf<Warmer>());
		}

		[Test]
		public void Create_WhenRetriesAreOne_ReturnWarmer()
		{
			var options = new Options
			{
				Retries = 1
			};

			var config = new Mock<IConfig>();
			var requester = new Mock<IRequester>();
			var logger = new Mock<ILogger>();

			var warmer = WarmerFactory.Create(options, config.Object, requester.Object, logger.Object);

			Assert.That(warmer, Is.InstanceOf<Warmer>());
		}

		[Test]
		public void Create_WhenRetriesAreMoreThanOne_ReturnRepeatWarmer()
		{
			var options = new Options
			{
				Retries = 2
			};

			var config = new Mock<IConfig>();
			var requester = new Mock<IRequester>();
			var logger = new Mock<ILogger>();

			var warmer = WarmerFactory.Create(options, config.Object, requester.Object, logger.Object);

			Assert.That(warmer, Is.InstanceOf<RepeatWarmer>());
		}
	}
}
