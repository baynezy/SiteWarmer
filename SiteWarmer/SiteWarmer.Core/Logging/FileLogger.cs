using System.Collections.Generic;
using System.Linq;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class FileLogger : ILogger
	{
		private static IList<Check> _checks;
		private static IDictionary<Check, bool> _tracker;
		private readonly IFileHelper _helper;
		private const string LogFile = "error.log";

		public FileLogger(IFileHelper helper)
		{
			_checks = new List<Check>();
			_tracker = new Dictionary<Check, bool>();
			_helper = helper;
		}

		public void Log(Check check)
		{
			if (check.Passed())
			{
				RemoveError(check);
			}
			else
			{
				AppendError(check);
			}
		}

		private static void RemoveError(Check check)
		{
			if (_tracker.ContainsKey(check))
			{
				_tracker.Remove(check);
			}
		}

		private static void AppendError(Check check)
		{
			if (_tracker.ContainsKey(check)) return;
			_checks.Add(check);
			_tracker.Add(check, true);
		}

		public void Close()
		{
			var failedChecks = FindFailedChecks();
            if (failedChecks.Count == 0) return;
            
            InitializeLog();
			foreach (var check in failedChecks)
			{
				_helper.WriteLine(LogFile, check.Url);
			}
		}

		private static IList<Check> FindFailedChecks()
		{
			return _checks.Where(check => _tracker.ContainsKey(check)).ToList();
		}

		private void InitializeLog()
		{
		    if (_helper.FileExists(LogFile)) return;

			_helper.CreateFile(LogFile);
		}
	}
}
