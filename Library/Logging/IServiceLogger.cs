using Library.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging
{
    public interface IServiceLogger<T> 
        where T : ServiceBase<T>
    {
        public void Log(string message);
        public void LogException(Exception ex);
    }
}
