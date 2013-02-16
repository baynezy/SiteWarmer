using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	public class Check
	{
		public static int Ok = 200;
		private string _source = "";
		private bool _passed;
		public string Url { get; set; }
		public int Status { get; set; }
		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}

		public IList<ContentMatch> ContentMatches { get; set; }

		public bool Passed()
		{
			return Status == Ok;
		}
	}
}
