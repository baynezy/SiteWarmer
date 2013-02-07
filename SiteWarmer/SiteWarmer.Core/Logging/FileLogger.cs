using System.Collections.Generic;
using System.IO;
using System.Linq;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class FileLogger : ILogger
	{
		private static IList<Check> _checks;
		private static IDictionary<Check, bool> _tracker;
		private const string LogFile = "error.log";

		public FileLogger()
		{
			_checks = new List<Check>();
			_tracker = new Dictionary<Check, bool>();
		}

		public void Log(Check check)
		{
			if (check.Status == Check.Ok) return;

			AppendError(check);
		}

		public void Close()
		{
            if (!AnyCheckErrored()) return;
            
            InitializeLog();
			using (var writer = File.AppendText(LogFile))
			{
				foreach (var check in _checks)
				{
					writer.WriteLine(check.Url);
				}
			}
		}

		private static void InitializeLog()
		{
		    if (FileExists(LogFile)) return;

			CreateFile(LogFile);
		}

	    private static bool AnyCheckErrored()
	    {
	        return _checks.Count != 0;
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
			if (!_tracker.ContainsKey(check))
			{
				_checks.Add(check);
				_tracker.Add(check, true);
			}
		}
	}
}
