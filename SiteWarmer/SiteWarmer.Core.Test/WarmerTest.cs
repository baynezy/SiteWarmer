using NUnit.Framework;

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

			Assert.AreEqual(5, checks.Count);
			Assert.AreEqual(200, checks[0].Status);
			Assert.AreEqual(200, checks[1].Status);
			Assert.AreEqual(500, checks[2].Status);
			Assert.AreEqual(200, checks[3].Status);
			Assert.AreEqual(200, checks[4].Status);
		}
	}
}
