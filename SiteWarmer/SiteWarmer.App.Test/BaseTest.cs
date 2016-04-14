using System;
using NUnit.Framework;

namespace SiteWarmer.App.Test
{
	abstract class BaseTest
	{
		protected string _testPath;

		[SetUp]
		public void SetUp()
		{
			_testPath = TestPath();
		}

		private static string TestPath()
		{
			// ReSharper disable ConvertToConstant.Local
			// ReSharper disable RedundantAssignment
			var fallback = @"..\..\..\..\";
			// ReSharper restore RedundantAssignment
			// ReSharper restore ConvertToConstant.Local
#if DEBUG
				fallback = @"..\..\..\";
#endif

			return Environment.GetEnvironmentVariable("Test.Path") ?? fallback;
		}
	}
}
