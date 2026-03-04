using SiteWarmer.Core.Collections;
using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging;

/// <summary>
/// Combine multiple ILogger implementations into a composite ILogger
/// </summary>
public class LoggerCollection : AbstractCollection<ILogger>, ILogger
{
    /// <inheritdoc/>
    public void Log(Check check)
    {
        foreach (var logger in Items)
        {
            logger.Log(check);
        }
    }

    /// <inheritdoc/>
    public void Close()
    {
        foreach (var logger in Items)
        {
            logger.Close();
        }
    }
}