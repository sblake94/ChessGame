using Library.Logging;

namespace Library.Services;

public class ServiceBase<T>
    where T : ServiceBase<T>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    private protected IServiceLogger<T>? _logger;

    public bool IsLoggingEnabled => _logger is not null;

    public ServiceBase(ILoggerFactoryService? loggerFactory = null)
    {
        var thisAsService = this as T;
        if (thisAsService is null) { return; }

        loggerFactory?.InsertServiceLogger(thisAsService);
    }

    public void SetLogger(IServiceLogger<T> logger)
    {
        _logger = logger;
    }
}
