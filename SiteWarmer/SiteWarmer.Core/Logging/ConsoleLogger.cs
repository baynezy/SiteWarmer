using System;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	/// <summary>
	/// Log Checks out to the console
	/// </summary>
	public class ConsoleLogger : ILogger
	{
		private static ConsoleColor _originalTextColour;
		private static readonly object LockObject = new object();
		private const ConsoleColor ErrorTextColour = ConsoleColor.Red;
		private const ConsoleColor ContentFailTextColour = ConsoleColor.Yellow;
		private const ConsoleColor SuccessTextColour = ConsoleColor.Green;

		/// <summary>
		/// Instantiate a new ConsoleLogger
		/// </summary>
		public ConsoleLogger()
		{
			_originalTextColour = Console.ForegroundColor;
		}

		public void Log(Check check)
		{
			var passed = (check.Passed()) ? "Passed" : "Failed";

			if (check.Passed())
			{
				WritePass(check, passed);
			}
			else if (!check.CheckContent())
			{
				WriteContentFail(check, passed);
			}
			else
			{
				WriteFail(check, passed);
			}
		}

		public void Close()
		{
			// no need to do anything here
		}

		private static void WritePass(Check check, string passed)
		{
			WriteToConsoleInColour(check, passed, SuccessTextColour);
		}

		private static void WriteContentFail(Check check, string passed)
		{
			WriteToConsoleInColour(check, passed, ContentFailTextColour);
		}

		private static void WriteFail(Check check, string passed)
		{
			WriteToConsoleInColour(check, passed, ErrorTextColour);
		}

		private static void WriteToConsoleInColour(Check check, string passed, ConsoleColor textColour)
		{
			// this is necessary as this can get called by multiple threads and it can cause the colours to get output incorrectly
			lock (LockObject)
			{
				Console.ForegroundColor = textColour;
				WriteToConsole(check, passed);
				Console.ForegroundColor = _originalTextColour;	
			}
		}

		private static void WriteToConsole(Check check, string passed)
		{
			Console.WriteLine("{0}: {1} - {2}", check.Status, check.Url, passed);
		}
	}
}
