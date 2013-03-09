using System.Reflection;

namespace SiteWarmer.App
{
	class Version
	{
		public static string GetAppVersion()
		{
			var assembly = Assembly.GetCallingAssembly();
			var version = assembly.GetName().Version;
			return version.ToString();
		}
	}
}
