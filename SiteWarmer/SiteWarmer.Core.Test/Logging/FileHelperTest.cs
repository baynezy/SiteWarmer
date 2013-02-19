using NUnit.Framework;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test.Logging
{
	[TestFixture]
	class FileHelperTest
	{
		[Test]
		public void FileHelper_ImplementsIHelper()
		{
			var fileHelper = new FileHelper();

			Assert.That(fileHelper, Is.InstanceOf<IFileHelper>());
		}
	}
}
