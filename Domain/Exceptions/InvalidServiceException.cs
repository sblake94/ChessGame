using System;

namespace Domain.Exceptions
{
    public class InvalidServiceException : Exception
    {
        Type abstraction;
        Type service;

        public InvalidServiceException(string message, Type @interface, Type implementation)
            : base(message)
        {
            abstraction = @interface;
            service = implementation;
        }
    }
}