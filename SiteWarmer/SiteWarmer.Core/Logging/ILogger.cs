using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	public interface ILogger
	{
		void Log(Check check);
		void Close();
	}
}
