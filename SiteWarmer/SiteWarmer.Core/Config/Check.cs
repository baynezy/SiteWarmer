using System.Collections.Generic;

namespace SiteWarmer.Core.Config
{
	/// <summary>
	/// This is a logical representation of a Url and its content matches
	/// as well as status and source Html
	/// </summary>
	public class Check
	{
		public static int Ok = 200;
		private string _source = "";
		private IList<ContentMatch> _contentMatches = new List<ContentMatch>();

		/// <summary>
		/// The Url to check
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The status code returned when the Url is hit
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// The Html source returned from the Url
		/// </summary>
		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}

		/// <summary>
		/// A collection of positive and negative content matches
		/// </summary>
		public IList<ContentMatch> ContentMatches
		{
			get { return _contentMatches; }
			set { _contentMatches = value; }
		}

		/// <summary>
		/// Can tell you if the check passed its checks
		/// </summary>
		/// <returns>Whether the Url passed the check</returns>
		public bool Passed()
		{
			return Status == Ok && CheckContent();
		}

		public bool CheckContent()
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
