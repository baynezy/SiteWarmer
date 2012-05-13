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
			_warmer = new Warmer(config, requester);
		}

		[Test]
		public void Warm()
		{
			var checks = _warmer.Warm();

			Assert.AreEqual(2, checks.Count);
			Assert.AreEqual(200, checks[0].Status);
			Assert.AreEqual(200, checks[1].Status);
		}
	}
}
