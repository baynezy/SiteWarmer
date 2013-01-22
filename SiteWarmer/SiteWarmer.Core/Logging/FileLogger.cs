using System.Collections.Generic;
using System.IO;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class FileLogger : ILogger
	{
		private static IList<Check> _checks;
		private const string LogFile = "error.log";

		public FileLogger()
		{
			_checks = new List<Check>();
		}

		public void Log(Check check)
		{
			if (check.Status == Check.Ok) return;

			AppendError(check);
		}

		public void Close()
		{
			InitializeLogIfNeccessary();
			using (var writer = File.AppendText(LogFile))
			{
				foreach (var check in _checks)
				{
					writer.WriteLine(check.Url);
				}
			}
		}

		private static void InitializeLogIfNeccessary()
		{
			if (!FileExists(LogFile))
			{
				CreateFile(LogFile);
			}
		}

		private static bool FileExists(string errorLog)
		{
			return File.Exists(Path.GetFullPath(errorLog));
		}

		private static void CreateFile(string errorLog)
		{
			var file = File.CreateText(Path.GetFullPath(errorLog));
			file.Close();
		}

		private static void AppendError(Check check)
		{
			_checks.Add(check);
		}
	}
}
