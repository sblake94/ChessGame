using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Library.Common
{
    // Note: This was written by ChatGPT, custom implementation logic pending
    public class DefaultLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            // You can return a disposable object if you need to implement scoped logging
            // Otherwise, you can return null or an empty disposable to indicate no scope
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // You can implement your own logic to determine if the logger is enabled for the specified log level
            // For simplicity, let's assume the logger is always enabled
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // You can implement the logic to perform the actual logging here
            // This method will be called when a log message needs to be written

            // Example implementation:
            // Print the log level, event ID, formatted message, and exception (if present)
            Console.WriteLine($"[{logLevel}] Event ID: {eventId} - {formatter(state, exception)}");
            if (exception != null)
            {
                Console.WriteLine($"Exception: {exception}");
            }
        }
    }
}