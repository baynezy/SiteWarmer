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

			Assert.That(config, Is.InstanceOf<IConfig>());
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

			Assert.That(checks.Count, Is.EqualTo(expectedNumberOfChecks));

			Assert.That(checks[0].Url, Is.EqualTo("http://www.yahoo.com/"));
			matches = checks[0].ContentMatches;
			Assert.That(matches.Count(m => m.Required), Is.EqualTo(expectedNumberOfPositiveContentMatches));
			Assert.That(matches.Count(m => !m.Required), Is.EqualTo(expectedNumberOfNegativeContentMatches));

			Assert.That(checks[1].Url, Is.EqualTo("http://www.google.com/"));
			matches = checks[1].ContentMatches;
			Assert.That(matches.Count(m => m.Required), Is.EqualTo(expectedNumberOfPositiveContentMatches));
			Assert.That(matches.Count(m => !m.Required), Is.EqualTo(0));

			Assert.That(checks[2].Url, Is.EqualTo("http://www.github.com/"));
			matches = checks[2].ContentMatches;
			Assert.That(matches.Count(m => m.Required), Is.EqualTo(expectedNumberOfPositiveContentMatches));
			Assert.That(matches.Count(m => !m.Required), Is.EqualTo(0));

			Assert.That(checks[3].Url, Is.EqualTo("http://www.bbc.co.uk/"));
			matches = checks[3].ContentMatches;
			Assert.That(matches.Count(m => m.Required), Is.EqualTo(expectedNumberOfPositiveContentMatches));
			Assert.That(matches.Count(m => !m.Required), Is.EqualTo(0));
		}

		[Test]
		public void Checks_ReadingInEmptyFile_ReturnsEmptyListOfChecks()
		{
			const int expectedNumberOfChecks = 0;
			var config = new XmlConfig(@"..\..\Data\empty.xml");

			var checks = config.Checks;

			Assert.That(checks.Count, Is.EqualTo(expectedNumberOfChecks));
		}

		[Test]
		public void Checks_ReadingInFileWithEmptyMatches_ReturnsEmptyListOfMatches()
		{
			const int expectedNumberOfMatches = 0;
			var config = new XmlConfig(@"..\..\Data\empty_items.xml");

			var checks = config.Checks;

			Assert.That(checks[0].ContentMatches.Count, Is.EqualTo(expectedNumberOfMatches));
		}
	}
}
