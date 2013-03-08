using System;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class ConsoleLogger : ILogger
	{
		private static ConsoleColor _originalTextColour;
		private const ConsoleColor ErrorTextColour = ConsoleColor.Red;
		private const ConsoleColor SuccessTextColour = ConsoleColor.Green;

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
			else
			{
				WriteFail(check, passed);
			}
		}

		private static void WritePass(Check check, string passed)
		{
			Console.ForegroundColor = SuccessTextColour;
			WriteToConsole(check, passed);
			Console.ForegroundColor = _originalTextColour;
		}

		private static void WriteFail(Check check, string passed)
		{
			Console.ForegroundColor = ErrorTextColour;
			WriteToConsole(check, passed);
			Console.ForegroundColor = _originalTextColour;
		}

		private static void WriteToConsole(Check check, string passed)
		{
			Console.WriteLine("{0}: {1} - {2}", check.Status, check.Url, passed);
		}

		public void Close()
		{
			// no need to do anything here
		}
	}
}
