using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    public class ServiceInterfaceNotFoundException : Exception
    {
        public Type serviceType;
        public ServiceInterfaceNotFoundException(string message, Type serviceImplementation)
            : base(message)
        {
            serviceType = serviceImplementation;
        }
    }
}
