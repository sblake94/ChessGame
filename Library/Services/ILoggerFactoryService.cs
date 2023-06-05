using Microsoft.Extensions.Logging;

namespace Library.Services;

public interface ILoggerFactoryService
{
    public void InsertServiceLogger<T>(T service) where T : ServiceBase<T>;
}