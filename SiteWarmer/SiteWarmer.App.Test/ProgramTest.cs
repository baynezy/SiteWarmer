using System;
using NUnit.Framework;
namespace SiteWarmer.App.Test
{
	[TestFixture]
	class ProgramTest
	{
		[Test]
		public void Main_WhenPassingMinimumArgsForLogging_ThenShouldNotThrowException()
		{
			var args = new[]
					{
						"-i",
						@"..\..\..\SiteWarmer.Core.Test\Data\urls.txt"
					};
			AssertThatProgramNotFailing(args, "When only passing in -i there should not be an error.");
		}

		[Test]
		public void Main_WhenPassingOnlyVersionArgument_ThenShouldNotThrowException()
		{
			var args = new[]
					{
						"--version"
					};
			AssertThatProgramNotFailing(args, "When only passing in --version there should not be an error.");
		}

		private static void AssertThatProgramNotFailing(string[] args, string message = "There should not be an exception here.")
		{
			try
			{
				Program.Main(args);
			}
			catch (Exception e)
			{
				Assert.Fail("{0} - Cause: {1}", message, e.Message);
			}
		}
	}
}
