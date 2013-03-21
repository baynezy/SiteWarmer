using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
{
	[TestFixture]
	class ConfigCollectionTest
	{
		[Test]
		public void ConfigCollection_ImplementsIConfig()
		{
			var config = new ConfigCollection();

			Assert.That(config, Is.InstanceOf<IConfig>());
		}

		[Test]
		public void Size_WhenConfigIsEmpty_ThenReturnZero()
		{
			var config = new ConfigCollection();

			Assert.That(config.Size(), Is.EqualTo(0));
		}

		[Test]
		public void Add_WhenAddingMultipleConfigs_ThenSizeShouldBeEqualToThat()
		{
			var config = new ConfigCollection();
			const int expectedSize = 2;

			config.Add(new Mock<IConfig>().Object);
			config.Add(new Mock<IConfig>().Object);

			Assert.That(config.Size(), Is.EqualTo(expectedSize));
		}

		[Test]
		public void Checks_WhenUsingMultipleConfigs_ThenChecksShouldReturnTheCombinationOfTheConfigs()
		{
			var configCollection = new ConfigCollection();
			var configOne = new Mock<IConfig>();

			const int expectedChecks = 4;

			configOne.Setup(m => m.Checks).Returns(
					new List<Check>
						{
							new Check
								{
									Url = "http://www.google.com/"
								},
							new Check
								{
									Url = "http://www.bbc.co.uk/"
								}
						}
				);

			var configTwo = new Mock<IConfig>();

			configTwo.Setup(m => m.Checks).Returns(
					new List<Check>
						{
							new Check
								{
									Url = "http://www.bing.com/"
								},
							new Check
								{
									Url = "http://www.yahoo.com/"
								}
						}
				);

			configCollection.Add(configOne.Object);
			configCollection.Add(configTwo.Object);

			var checks = configCollection.Checks;

			Assert.That(checks.Count, Is.EqualTo(expectedChecks));
			Assert.That(checks[0].Url, Is.EqualTo("http://www.google.com/"));
			Assert.That(checks[1].Url, Is.EqualTo("http://www.bbc.co.uk/"));
			Assert.That(checks[2].Url, Is.EqualTo("http://www.bing.com/"));
			Assert.That(checks[3].Url, Is.EqualTo("http://www.yahoo.com/"));
		}
	}
}
