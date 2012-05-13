using NUnit.Framework;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class RequesterTest
	{
		private Requester _requester;

		[SetUp]
		public void SetUp()
		{
			_requester = new Requester();
		}

		[Test]
		public void TestUrl()
		{
			var check = new Check {Url = "http://www.simonbaynes.com/"};

			_requester.Check(check);

			Assert.AreEqual(200, check.Status);
		}
	}
}
