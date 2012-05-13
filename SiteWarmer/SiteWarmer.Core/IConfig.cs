using System.Collections.Generic;

namespace SiteWarmer.Core
{
	public interface IConfig
	{
		List<Check> Checks { get; }
	}
}
