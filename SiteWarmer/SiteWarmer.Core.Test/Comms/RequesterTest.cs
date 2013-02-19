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

			Assert.That(check.Status, Is.EqualTo(Check.Ok));
		}

		[Test]
        public void Check_CallsErroringPage_Returns500StatusCode()
		{
			var check = new Check { Url = "http://www.simonbaynes.com/_errors/error.cfm" };

			_requester.Check(check);

			Assert.That(check.Status, Is.EqualTo(500));
		}

		[Test]
        public void Check_CallsMissingPage_Returns404StatusCode()
		{
			var check = new Check { Url = "http://www.simonbaynes.com/404/" };

			_requester.Check(check);

			Assert.That(check.Status, Is.EqualTo(404));
		}

		[Test]
		public void Check_CallsExistingPage_ReturnsCorrectContent()
		{
			var check = new Check { Url = "http://www.github.com/" };

			_requester.Check(check);

			AssertContainsString("<title>GitHub · Build software better, together.</title>", check.Source);
		}

		private static void AssertContainsString(string expected, string actual)
		{
			var match = actual.IndexOf(expected, System.StringComparison.Ordinal);

			Assert.AreNotEqual(-1, match, string.Format("'{0}' was not present in the source", expected));
		}
	}
}
