using System.Collections.Generic;
using SiteWarmer.Core.Collection;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public class LoggerCollection : AbstractCollection<ILogger>, ILogger
	{
		
		public void Log(Check check)
		{
			foreach (var logger in Items)
			{
				logger.Log(check);
			}
		}

		public void Close()
		{
			foreach (var logger in Items)
			{
				logger.Close();
			}
		}
	}
}
