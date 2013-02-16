using System.Collections.Generic;
using NUnit.Framework;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Test.Config
{
	[TestFixture]
	class CheckTest
	{
		[Test]
		public void Passed_WhenStatusCodeIs200AndThereAreNoContentChecks_ThenReturnTrue()
		{
			var check = new Check
				{
					Status = Check.Ok
				};

			Assert.True(check.Passed());
		}

		[Test]
		public void Passed_WhenStatusCodeIsNot200_ThenReturnFalse()
		{
			var check = new Check
			{
				Status = 500
			};

			Assert.False(check.Passed());
		}

		[Test]
		public void Passed_WhenStatusCodeIs200AndAContentMatchFails_ThenReturnFalse()
		{
			var check = new Check
			{
				Status = Check.Ok,
				Source = "This is the test source.",
				ContentMatches = new List<ContentMatch>
						{
							new ContentMatch
								{
									Match = "match"
								}
						}
			};

			Assert.False(check.Passed());
		}

		[Test]
		public void Passed_WhenStatusCodeIs200AndAContentMatchPasses_ThenReturnTrue()
		{
			var check = new Check
			{
				Status = Check.Ok,
				Source = "This is the test source containing a match.",
				ContentMatches = new List<ContentMatch>
						{
							new ContentMatch
								{
									Match = "match"
								}
						}
			};

			Assert.True(check.Passed());
		}
	}
}
