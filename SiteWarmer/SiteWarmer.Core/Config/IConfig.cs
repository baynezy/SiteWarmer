using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	/// <summary>
	/// Classes implementing this Interface represent a collection of checks
	/// tht will be tested
	/// </summary>
	public interface IConfig
	{
		/// <summary>
		/// The collection of Checks contained within the config
		/// </summary>
		IList<Check> Checks { get; }
	}
}
