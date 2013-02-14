using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	public class Check
	{
		public static int Ok = 200;
		public string Url { get; set; }
		public int Status { get; set; }
		public IList<ContentMatch> ContentMatches { get; set; }
	}
}
