using NUnit.Framework;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class FileLoggerTest
	{
		[Test]
		public void ImplementsILogger()
		{
			var logger = new FileLogger();

			Assert.IsInstanceOf(typeof(ILogger), logger);
		}
	}
}
