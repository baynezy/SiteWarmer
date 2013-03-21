using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SiteWarmer.Core.Config
{
	public class XmlConfig : IConfig
	{
		private readonly IList<Check> _checks;
		private readonly string _configPath;

		public XmlConfig(string configPath)
		{
			_configPath = configPath;
			_checks = new List<Check>();
			Load();
		}

		private void Load()
		{
			var doc = new XmlDocument();
			doc.Load(_configPath);

			ParseXml(doc);
		}

		private void ParseXml(XmlNode doc)
		{
			var nodes = doc.SelectNodes("/checks/check");

			HandleNodes(nodes);
		}

		private void HandleNodes(IEnumerable nodes)
		{
			foreach (XmlNode node in nodes)
			{
				var urls = node.SelectNodes("url");
				var check = new Check();

				if (urls == null || urls.Count == 0) return;

				check.Url = urls[0].InnerText;

				var positiveChecks = node.SelectNodes("content/positive");
				var negativeChecks = node.SelectNodes("content/negative");

				var positiveMatches = (from XmlNode item in positiveChecks select new ContentMatch { Match = item.InnerText, Required = true}).ToList();
				var negativeMatches = (from XmlNode item in negativeChecks select new ContentMatch { Match = item.InnerText, Required = false }).ToList();

				check.ContentMatches = MergeMatches(positiveMatches, negativeMatches);

				_checks.Add(check);
			}
		}

		private static IList<ContentMatch> MergeMatches(ICollection<ContentMatch> positiveMatches, ICollection<ContentMatch> negativeMatches)
		{
			var matches = new List<ContentMatch>(positiveMatches.Count + negativeMatches.Count);

			matches.AddRange(positiveMatches);
			matches.AddRange(negativeMatches);

			return matches;
		}

		public IList<Check> Checks
		{
			get { return _checks; }
		}
	}
}