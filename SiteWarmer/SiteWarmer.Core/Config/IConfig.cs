using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	public interface IConfig
	{
		List<Check> Checks { get; }
	}
}
