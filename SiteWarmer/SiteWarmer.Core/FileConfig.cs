using System.Collections.Generic;
using System.IO;

namespace SiteWarmer.Core
{
	public class FileConfig : IConfig
	{
		private List<Check> _checks;

		public FileConfig(string configPath)
		{
			_checks = new List<Check>();
			Load(configPath);
		}

		public List<Check> Checks
		{
			get { return _checks; }
		}

		private void Load(string configPath)
		{
			var fullPath = Path.GetFullPath(configPath);
			ParseFile(fullPath);
		}

		private void ParseFile(string fullPath)
		{
			var file = new StreamReader(fullPath);
			string line;

			while ((line = file.ReadLine()) != null)
			{
				AddCheck(line);
			}
		}

		private void AddCheck(string line)
		{
			var check = new Check {Url = line.Trim()};
			_checks.Add(check);
		}
	}
}
