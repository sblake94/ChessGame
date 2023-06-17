using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string argumentName = null)
            : base($"Argument {argumentName} was invalid")
        {
            
        }
    }
}
