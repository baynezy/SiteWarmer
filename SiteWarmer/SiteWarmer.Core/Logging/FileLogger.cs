using System.Collections.Generic;
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
			if (check.Passed()) return;

			AppendError(check);
		}

		public void Close()
		{
            if (!AnyCheckErrored()) return;
            
            InitializeLog();
			foreach (var check in _checks)
			{
				_helper.WriteLine(LogFile, check.Url);
			}
		}

		private void InitializeLog()
		{
		    if (_helper.FileExists(LogFile)) return;

			_helper.CreateFile(LogFile);
		}

	    private static bool AnyCheckErrored()
	    {
	        return _checks.Count != 0;
	    }

		private static void AppendError(Check check)
		{
			if (_tracker.ContainsKey(check)) return;
			_checks.Add(check);
			_tracker.Add(check, true);
		}
	}
}
