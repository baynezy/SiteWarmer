using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
{
	[TestFixture]
	class XmlConfigTest
	{
		[Test]
		public void XmlConfig_ImplementsIConfig()
		{
			var config = new XmlConfig(@"..\..\Data\urls.xml");

			Assert.IsInstanceOf<IConfig>(config);
		}

		[Test]
		public void Checks_ReadingInCorrectFile_ReturnsChecksCorrectly()
		{
			const int expectedNumberOfChecks = 4;
			const int expectedNumberOfContentMatches = 1;
			var config = new XmlConfig(@"..\..\Data\urls.xml");

			var checks = config.Checks;

			Assert.AreEqual(expectedNumberOfChecks, checks.Count);

			Assert.AreEqual("http://www.yahoo.com/", checks[0].Url);
			Assert.AreEqual(expectedNumberOfContentMatches, checks[0].ContentMatches.Count);

			Assert.AreEqual("http://www.google.com/", checks[1].Url);
			Assert.AreEqual(expectedNumberOfContentMatches, checks[1].ContentMatches.Count);


			Assert.AreEqual("http://www.github.com/", checks[2].Url);
			Assert.AreEqual(expectedNumberOfContentMatches, checks[2].ContentMatches.Count);


			Assert.AreEqual("http://www.bbc.co.uk/", checks[3].Url);
			Assert.AreEqual(expectedNumberOfContentMatches, checks[3].ContentMatches.Count);


		}

		[Test]
		public void Checks_ReadingInEmptyFile_ReturnsEmptyListOfChecks()
		{
			const int expectedNumberOfChecks = 0;
			var config = new XmlConfig(@"..\..\Data\empty.xml");

			var checks = config.Checks;

			Assert.AreEqual(expectedNumberOfChecks, checks.Count);
		}

		[Test]
		public void Checks_ReadingInFileWithEmptyMatches_ReturnsEmptyListOfMatches()
		{
			const int expectedNumberOfMatches = 0;
			var config = new XmlConfig(@"..\..\Data\empty_items.xml");

			var checks = config.Checks;

			Assert.AreEqual(expectedNumberOfMatches, checks[0].ContentMatches.Count);
		}
	}
}
