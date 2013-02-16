using System.Collections.Generic;
using System.Linq;
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
			const int expectedNumberOfNegativeContentMatches = 1;
			const int expectedNumberOfPositiveContentMatches = 1;
			var config = new XmlConfig(@"..\..\Data\urls.xml");

			var checks = config.Checks;

			IList<ContentMatch> matches;

			Assert.AreEqual(expectedNumberOfChecks, checks.Count);

			Assert.AreEqual("http://www.yahoo.com/", checks[0].Url);
			matches = checks[0].ContentMatches;
			Assert.AreEqual(expectedNumberOfPositiveContentMatches, matches.Count(m => m.Required));
			Assert.AreEqual(expectedNumberOfNegativeContentMatches, matches.Count(m => !m.Required));

			Assert.AreEqual("http://www.google.com/", checks[1].Url);
			matches = checks[1].ContentMatches;
			Assert.AreEqual(expectedNumberOfPositiveContentMatches, matches.Count(m => m.Required));
			Assert.AreEqual(0, matches.Count(m => !m.Required));

			Assert.AreEqual("http://www.github.com/", checks[2].Url);
			matches = checks[2].ContentMatches;
			Assert.AreEqual(expectedNumberOfPositiveContentMatches, matches.Count(m => m.Required));
			Assert.AreEqual(0, matches.Count(m => !m.Required));

			Assert.AreEqual("http://www.bbc.co.uk/", checks[3].Url);
			matches = checks[3].ContentMatches;
			Assert.AreEqual(expectedNumberOfPositiveContentMatches, matches.Count(m => m.Required));
			Assert.AreEqual(0, matches.Count(m => !m.Required));
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
