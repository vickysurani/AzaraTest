namespace azara.logs.implementations;

public class LogManager : ILogManager, IDisposable
{
    public void LogCritical(string Message, string Details = null) => throw new NotImplementedException();

    public void LogDebug(string Message, string Details = null) => throw new NotImplementedException();

    public void LogError(string Message, Exception Details = null) => throw new NotImplementedException();

    public void LogInfo(string Message, string Details = null) => throw new NotImplementedException();

    public void LogTrace(string Message, string Details = null) => throw new NotImplementedException();

    public void LogWarning(string Message, string Details = null) => throw new NotImplementedException();

    #region Dispose

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion Dispose
}