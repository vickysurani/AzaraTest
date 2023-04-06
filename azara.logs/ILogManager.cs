namespace azara.logs;

public interface ILogManager : IDisposable
{
    /// <summary>
    /// Log information in the database.
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogInfo(string Message, string Details = null);

    /// <summary>
    /// Log Error in the database
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogError(string Message, Exception Details = null);

    /// <summary>
    /// Log Warning in the database
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogWarning(string Message, string Details = null);

    /// <summary>
    /// Log Debug in the database
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogDebug(string Message, string Details = null);

    /// <summary>
    /// Log Critical in the database
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogCritical(string Message, string Details = null);

    /// <summary>
    /// Log Trace in the database
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Details"></param>
    void LogTrace(string Message, string Details = null);
}