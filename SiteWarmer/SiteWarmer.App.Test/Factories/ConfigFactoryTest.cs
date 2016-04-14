using System.Collections.Generic;
using NUnit.Framework;
using SiteWarmer.App.Factories;
using SiteWarmer.Core.Config;

namespace SiteWarmer.App.Test.Factories
{
	[TestFixture]
	class ConfigFactoryTest : BaseTest
	{
		[Test]
		public void Create_WhenConfigFileHasXmlExtension_ThenReturnXmlConfig()
		{
			var files = new List<string> { _testPath + @"SiteWarmer.Core.Test\Data\urls.xml" };
			var options = new Options { Inputfiles = files };

			var config = ConfigFactory.Create(options);

			Assert.That(config, Is.InstanceOf<XmlConfig>());
		}

		[Test]
		public void Create_WhenConfigFileDoesNotHaveAnXmlExtension_ThenReturnFileConfig()
		{
			var files = new List<string> { _testPath + @"SiteWarmer.Core.Test\Data\urls.txt" };
			var options = new Options { Inputfiles = files };

			var config = ConfigFactory.Create(options);

			Assert.That(config, Is.InstanceOf<FileConfig>());
		}

		[Test]
		public void Create_WhenThereAreMultipleFiles_ThenCreateConfigCollection()
		{
			const int expectedCount = 2;
			var files = new List<string>
				{
					_testPath + @"SiteWarmer.Core.Test\Data\urls.txt",
					_testPath + @"SiteWarmer.Core.Test\Data\urls.xml"
				};

			var options = new Options { Inputfiles = files };

			var config = ConfigFactory.Create(options) as ConfigCollection;

			Assert.IsNotNull(config);

			Assert.That(config.Size(), Is.EqualTo(expectedCount));
		}
	}
}
