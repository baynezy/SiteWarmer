using System;
using System.Net;

namespace SiteWarmer.Core
{
	public class Requester
	{
		public void Check(Check check)
		{
			check.Status = GetStatus(check.Url);
		}

		private static int GetStatus(string url)
		{
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.Timeout = 10000;
			try
			{
				var response = (HttpWebResponse) request.GetResponse();
				return (int) response.StatusCode;
			}
			catch (WebException e)
			{
				return 500;
			}
		}
	}
}
