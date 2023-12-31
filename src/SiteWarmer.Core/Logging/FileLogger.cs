using SiteWarmer.Core.Config;

namespace SiteWarmer.Core.Logging;

/// <summary>
/// Log Checks out to a file 
/// </summary>
public class FileLogger : ILogger
{
    private readonly List<Check> _checks = [];
    private readonly Dictionary<Check, bool> _tracker = new();
    private readonly IFileHelper _helper;
    private const string LogFile = "error.log";

    /// <summary>
    /// Instantiate new FileLogger
    /// </summary>
    /// <param name="helper">File helper</param>
    public FileLogger(IFileHelper helper)
    {
        _helper = helper;
    }

    public void Log(Check check)
    {
        if (check.Passed())
        {
            RemoveError(check);
        }
        else
        {
            AppendError(check);
        }
    }

    private void RemoveError(Check check)
    {
        _tracker.Remove(check);
    }

    private void AppendError(Check check)
    {
        if (_tracker.ContainsKey(check)) return;
        _checks.Add(check);
        _tracker.Add(check, true);
    }

    public void Close()
    {
        var failedChecks = FindFailedChecks();
        if (failedChecks.Count == 0) return;
            
        InitializeLog();
        foreach (var check in failedChecks)
        {
            _helper.WriteLine(LogFile, check.Url);
        }
    }

    private List<Check> FindFailedChecks()
    {
        return _checks.Where(check => _tracker.ContainsKey(check)).ToList();
    }

    private void InitializeLog()
    {
        if (_helper.FileExists(LogFile)) return;

        _helper.CreateFile(LogFile);
    }
}