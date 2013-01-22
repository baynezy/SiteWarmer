using System;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class ConsoleLogger : ILogger
	{
		public void Log(Check check)
		{
			Console.WriteLine(String.Format("{0}: {1}", check.Status, check.Url));
		}

		public void Close()
		{
			// no need to do anything here
		}
	}
}
