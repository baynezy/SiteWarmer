using NUnit.Framework;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class FileConfigTest
	{
		private IConfig _config;

		[SetUp]
		public void SetUp()
		{
			_config = new FileConfig(@"..\..\Data\urls.txt");
		}

		[Test]
		public void Checks()
		{
			var checks = _config.Checks;
			Assert.AreEqual(3, checks.Count);
			Assert.AreEqual("http://www.simonbaynes.com/", checks[0].Url);
			Assert.AreEqual("http://www.simonbaynes.com/blog/", checks[1].Url);
			Assert.AreEqual("http://www.simonbaynes.com/_errors/error.cfm", checks[2].Url);
		}
	}
}
