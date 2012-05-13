using System.Collections.Generic;
using NUnit.Framework;

namespace SiteWarmer.Core.Test
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
		public void Checks()
		{
			var checks = _config.Checks;
			Assert.AreEqual(3, checks.Count);
			Assert.AreEqual("http://www.simonbaynes.com/", checks[0].Url);
		}
	}
}
