using System.Collections.Generic;

namespace SiteWarmer.Core
{
	public class LoggerCollection : ILogger
	{
		private readonly IList<ILogger> _loggers;

		public LoggerCollection()
		{
			_loggers = new List<ILogger>();
		}

		public int Size()
		{
			return _loggers.Count;
		}

		public void Add(ILogger logger)
		{
			_loggers.Add(logger);
		}

		public void Log(Check check)
		{
			foreach (var logger in _loggers)
			{
				logger.Log(check);
			}
		}
	}
}
