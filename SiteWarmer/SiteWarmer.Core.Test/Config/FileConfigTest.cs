using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
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
		public void Checks_ReadingInCorrectFile_ReturnsChecksCorrectly()
		{
			var checks = _config.Checks;
			Assert.AreEqual(4, checks.Count);
			Assert.AreEqual("http://www.yahoo.com/", checks[0].Url);
			Assert.AreEqual("http://www.google.com/", checks[1].Url);
			Assert.AreEqual("http://www.github.com/", checks[2].Url);
			Assert.AreEqual("http://www.bbc.co.uk/", checks[3].Url);
		}
	}
}
