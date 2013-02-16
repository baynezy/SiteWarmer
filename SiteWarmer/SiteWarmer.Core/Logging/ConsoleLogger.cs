using System;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class ConsoleLogger : ILogger
	{
		public void Log(Check check)
		{
			var passed = (check.Passed()) ? "Passed" : "Failed";
			Console.WriteLine(String.Format("{0}: {1} - {2}", check.Status, check.Url, passed));
		}

		public void Close()
		{
			// no need to do anything here
		}
	}
}
