using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging
{
	/// <summary>
	/// Classes that implement this Interfaces log the results of a Check
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logs the results of HTML calls to the Check passed in
		/// </summary>
		/// <param name="check">The Check we are logging</param>
		void Log(Check check);
		void Close();
	}
}
