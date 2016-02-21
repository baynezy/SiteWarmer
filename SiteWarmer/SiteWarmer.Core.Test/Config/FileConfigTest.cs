using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
{
	[TestFixture]
	class FileConfigTest
	{
		private IConfig _config;
		private string _testPath;

		[SetUp]
		public void SetUp()
		{
			_testPath = TestPath();
			_config = new FileConfig(_testPath + @"SiteWarmer.Core.Test\Data\urls.txt");
		}

		private static string TestPath()
		{
			return System.Environment.GetEnvironmentVariable("Test.Path") ?? @"..\..\..\";
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
