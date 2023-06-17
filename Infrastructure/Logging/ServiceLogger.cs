using Infrastructure.ServiceImplementations;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Infrastructure.Logging
{
    public class ServiceLogger<T>
        : IServiceLogger<T>
        , ILogger<T>
    {
        public ServiceLogger()
        {
            //if (typeof(T).BaseType != typeof(ServiceBase<>)) { throw new InvalidCastException($"{typeof(T).Name} to {typeof(ServiceBase<>)}"); }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
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
}