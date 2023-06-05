using Library.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Logging;

public class ServiceLogger<T> 
    : IServiceLogger<T>
    , ILogger<T>
    where T : ServiceBase<T>
{
    public ServiceLogger()
    {
        
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Debug.WriteLine(IsEnabled(logLevel) ? formatter(state, exception) : string.Empty);
    }

    public void Log(string message)
    {
        Debug.WriteLine(message);
    }

    public void LogException(Exception ex)
    {
        Log(ex.Message);
    }
}
