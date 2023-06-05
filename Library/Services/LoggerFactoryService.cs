using Library.Attributes.ServiceAttributes;
using Library.Logging;

namespace Library.Services;

[SingletonService]
public class LoggerFactoryService 
    : ServiceBase<LoggerFactoryService>
    , ILoggerFactoryService
    , IDisposable
{
    private Dictionary<Type, object> loggers = new Dictionary<Type, object>();

    public LoggerFactoryService()
    {

    }

    public void Dispose()
    {
        loggers.Clear();
    }

    public void InsertServiceLogger<T>(T service) where T : ServiceBase<T>
    {
        IServiceLogger<T> logger = new ServiceLogger<T>();
        service.SetLogger(logger);

        loggers.Add(typeof(T), logger);
    }

    public IServiceLogger<T> GetServiceLogger<T>() where T : ServiceBase<T>
    {
        return (IServiceLogger<T>)loggers[typeof(T)];
    }
}
