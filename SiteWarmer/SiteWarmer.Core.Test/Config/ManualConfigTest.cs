using System.Collections.Generic;
using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
{
	[TestFixture]
	class ManualConfigTest
	{
		[Test]
		public void ManualConfig_ImplementsIConfig()
		{
			var config = new ManualConfig();

			Assert.That(config, Is.InstanceOf<IConfig>());
		}

		[Test]
		public void Constructor_WhenInitialisingWithAListOfUrlStrings_ThenShouldCreateChecks()
		{
			var urls = new List<string>
				{
					"http://www.google.co.uk/",
					"http://www.bbc.co.uk/"
				};
			var config = new ManualConfig(urls);

			var checks = config.Checks;

			Assert.That(checks.Count, Is.EqualTo(urls.Count));
			Assert.That(checks[0].Url, Is.EqualTo(urls[0]));
			Assert.That(checks[1].Url, Is.EqualTo(urls[1]));
		}

		[Test]
		public void Constructor_WhenInitialisingWithAListOfChecks_ThenShouldStoreChecks()
		{
			var urls = new List<Check>
				{
					new Check
						{
							Url = "http://www.github.com/",
						},
					new Check
						{
							Url = "http://www.bbc.co.uk/"
						}
				};

			var config = new ManualConfig(urls);

			var checks = config.Checks;

			Assert.That(checks.Count, Is.EqualTo(urls.Count));
			Assert.That(checks[0].Url, Is.EqualTo(urls[0].Url));
			Assert.That(checks[1].Url, Is.EqualTo(urls[1].Url));
		}

		[Test]
		public void Add_WhenAddingChecks_ThenChecksPropertyShouldReturnTheCollection()
		{
			var config = new ManualConfig();

			const int expectedCount = 2;
			const string url1 = "http://www.bbc.co.uk/";
			const string url2 = "http://www.google.co.uk/";

			config.Add(new Check { Url = url1 });
			config.Add(new Check { Url = url2 });

			var checks = config.Checks;

			Assert.That(checks.Count, Is.EqualTo(expectedCount));
			Assert.That(checks[0].Url, Is.EqualTo(url1));
			Assert.That(checks[1].Url, Is.EqualTo(url2));
		}
	}
}
