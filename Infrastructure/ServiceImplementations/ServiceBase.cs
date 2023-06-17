using Application.ServiceAbstracts;
using Infrastructure.Logging;
using System;

namespace Infrastructure.ServiceImplementations
{
    public class ServiceBase<T> 
        : IServiceBase
        where T : class
    {
        public Guid Id { get; } = Guid.NewGuid();
        private protected IServiceLogger<T> _logger;

        public bool IsLoggingEnabled => !(_logger is null);

        public ServiceBase(ILoggerFactoryService loggerFactory = null)
        {
            var thisAsService = this as IServiceBase;
            if (thisAsService is null) { return; }

            loggerFactory?.InsertServiceLogger(this);
        }

        public ServiceBase()
        {
        }

        public void SetLogger(IServiceLogger<T> logger)
        {
            _logger = logger;
        }
    }
}