using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms
{
	public interface IRequester
	{
		/// <summary>
		/// Requests the Url contained within the Check and updates the Check with the 
		/// StatusCode and HTML Source of the Url
		/// </summary>
		/// <param name="check">The Check we wish to request</param>
		void Check(Check check);
	}
}
