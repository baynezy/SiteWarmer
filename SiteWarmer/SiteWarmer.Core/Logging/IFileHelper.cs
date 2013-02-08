namespace SiteWarmer.Core.Logging
{
	public interface IFileHelper
	{
		void WriteLine(string fileName, string text);
		bool FileExists(string fileName);
		void CreateFile(string fileName);
	}
}
