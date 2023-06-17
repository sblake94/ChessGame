using Application.ServiceAbstracts;
using Infrastructure.Attributes.ServiceAttributes;
using Infrastructure.Logging;
using System;
using System.Collections.Generic;

namespace Infrastructure.ServiceImplementations
{
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

        void ILoggerFactoryService.InsertServiceLogger(IServiceBase service)
        {
            Type serviceType = service.GetType();
            Type loggerType = typeof(ServiceLogger<>).MakeGenericType(serviceType);

            if(!loggers.ContainsKey(serviceType))
            {
                object logger = Activator.CreateInstance(loggerType);
                loggers.Add(serviceType, logger);
            }
        }

        public IServiceLogger<T> GetServiceLogger<T>() where T : ServiceBase<T>
        {
            return (IServiceLogger<T>)loggers[typeof(T)];
        }
    }
}