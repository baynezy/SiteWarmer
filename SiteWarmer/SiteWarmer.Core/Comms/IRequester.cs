using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms
{
	public interface IRequester
	{
		void Check(Check check);
	}
}
