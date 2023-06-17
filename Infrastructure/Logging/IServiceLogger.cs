using Infrastructure.ServiceImplementations;
using System;

namespace Infrastructure.Logging
{
    public interface IServiceLogger<T> 
    {
        void Log(string message);
        void LogException(Exception ex);
    }
}
