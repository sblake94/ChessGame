namespace Library.Exceptions;

public class InvalidServiceException : Exception
{
    Type? service;

    public InvalidServiceException(string? message, Type? @interface, Type? implementation)
        : base(message)
    {
        this.service = service;
    }
}
