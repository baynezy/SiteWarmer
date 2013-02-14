using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SiteWarmer.Core.Config
{
	public class XmlConfig : IConfig
	{
		private readonly List<Check> _checks;
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

		private void HandleNodes(XmlNodeList nodes)
		{
			foreach (XmlNode node in nodes)
			{
				var urls = node.SelectNodes("url");
				var check = new Check();

				if (urls == null || urls.Count == 0) return;

				check.Url = urls[0].InnerText;

				var checks = node.SelectNodes("content/item");

				check.ContentMatches = (from XmlNode item in checks select new ContentMatch { Match = item.InnerText }).ToList();

				_checks.Add(check);
			}
		}

		public List<Check> Checks
		{
			get { return _checks; }
		}
	}
}