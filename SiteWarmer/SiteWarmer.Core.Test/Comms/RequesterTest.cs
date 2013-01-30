using NUnit.Framework;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Comms
{
	[TestFixture]
	class RequesterTest
	{
		private IRequester _requester;

		[SetUp]
		public void SetUp()
		{
			_requester = new Requester();
		}

		[Test]
		public void Check_CallsExistingPage_Returns200StatusCode()
		{
			var check = new Check { Url = "http://www.simonbaynes.com/home/" };

			_requester.Check(check);

			Assert.AreEqual(200, check.Status);
		}

		[Test]
        public void Check_CallsErroringPage_Returns500StatusCode()
		{
			var check = new Check { Url = "http://www.simonbaynes.com/_errors/error.cfm" };

			_requester.Check(check);

			Assert.AreEqual(500, check.Status);
		}

		[Test]
        public void Check_CallsMissingPage_Returns404StatusCode()
		{
			var check = new Check { Url = "http://www.simonbaynes.com/404/" };

			_requester.Check(check);

			Assert.AreEqual(404, check.Status);
		}
	}
}
