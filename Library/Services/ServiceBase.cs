using Microsoft.Extensions.Logging;

namespace Library.Services;

public class ServiceBase<T>
    where T : ServiceBase<T>
{
    private readonly ILogger<T>? _logger;

    public ServiceBase(ILogger<T> logger)
    {
        _logger = logger;
    }
}
