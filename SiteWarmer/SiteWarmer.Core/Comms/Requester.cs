using System.IO;
using System.Net;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Comms
{
	public class Requester : IRequester
	{
		public void Check(Check check)
		{
			RunCheck(check);
		}

		private static void RunCheck(Check check)
		{
			var request = (HttpWebRequest) WebRequest.Create(check.Url);
			request.Timeout = 10000;
			HttpWebResponse response = null;
			try
			{
				response = ParseRequest(request, check);
			}
			finally
			{
				if (response != null) response.Close();
			}
		}

		private static HttpWebResponse ParseRequest(WebRequest request, Check check)
		{
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException e)
			{
				response = ((HttpWebResponse)e.Response);
			}

			if (response == null)
			{
				check.Status = 500;
			}
			else
			{
				check.Status = (int)response.StatusCode;
				check.Source = GetSource(response);
			}

			return response;
		}

		private static string GetSource(WebResponse response)
		{
			using (var stream = response.GetResponseStream())
			{
				var html = "";
				if (stream != null)
				{
					using (var reader = new StreamReader(stream))
					{
						html = reader.ReadToEnd();
					}
				}

				return html;
			}
		}
	}
}
