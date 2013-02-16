using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	public class Check
	{
		public static int Ok = 200;
		private string _source = "";
		private IList<ContentMatch> _contentMatches = new List<ContentMatch>();
		public string Url { get; set; }
		public int Status { get; set; }
		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}

		public IList<ContentMatch> ContentMatches
		{
			get { return _contentMatches; }
			set { _contentMatches = value; }
		}

		public bool Passed()
		{
			return Status == Ok && CheckContent();
		}

		private bool CheckContent()
		{
			var passed = true;
			
			foreach (var match in ContentMatches)
			{
				passed = CheckMatch(match);

				if (!passed)
				{
					break;
				}
			}

			return passed;
		}

		private bool CheckMatch(ContentMatch match)
		{
			var found = Source.IndexOf(match.Match, System.StringComparison.Ordinal) != -1;

			return found == match.Required;
		}
	}
}
