using SiteWarmer.Core.Collections;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging;

/// <summary>
/// Combine multiple ILogger implementations into a composite ILogger
/// </summary>
public class LoggerCollection : AbstractCollection<ILogger>, ILogger
{
		
    public void Log(Check check)
    {
        foreach (var logger in Items)
        {
            logger.Log(check);
        }
    }

    public void Close()
    {
        foreach (var logger in Items)
        {
            logger.Close();
        }
    }
}