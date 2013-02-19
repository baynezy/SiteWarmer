using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.App.Test
{
	[TestFixture]
	class ConfigFactoryTest
	{
		[Test]
		public void Create_WhenConfigFileHasXmlExtension_ThenReturnXmlConfig()
		{
			var options = new Options
				{
					Inputfile = @"..\..\..\SiteWarmer.Core.Test\Data\urls.xml"
				};

			var config = ConfigFactory.Create(options);

			Assert.That(config, Is.InstanceOf<XmlConfig>());
		}

		[Test]
		public void Create_WhenConfigFileDoesNotHaveAnXmlExtension_ThenReturnFileConfig()
		{
			var options = new Options
			{
				Inputfile = @"..\..\..\SiteWarmer.Core.Test\Data\urls.txt"
			};

			var config = ConfigFactory.Create(options);

			Assert.That(config, Is.InstanceOf<FileConfig>());
		}
	}
}
