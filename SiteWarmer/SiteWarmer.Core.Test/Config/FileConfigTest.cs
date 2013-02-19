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
			Assert.That(checks.Count, Is.EqualTo(4));
			Assert.That(checks[0].Url, Is.EqualTo("http://www.yahoo.com/"));
			Assert.That(checks[1].Url, Is.EqualTo("http://www.google.com/"));
			Assert.That(checks[2].Url, Is.EqualTo("http://www.github.com/"));
			Assert.That(checks[3].Url, Is.EqualTo("http://www.bbc.co.uk/"));
		}
	}
}
