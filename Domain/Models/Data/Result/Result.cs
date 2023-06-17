using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Models.Data.Result
{
    public abstract class Result<T>
    {
        public abstract bool IsSuccess { get; }
        public abstract T Value { get; }
        public abstract List<Exception> Exceptions { get; }
        public abstract AggregateException ThrowableException { get; }

        public static implicit operator Result<T>(T value)
        {
            return new Success<T>(value);
        }

        public static implicit operator Result<T>(Exception exception)
        {
            return new Failure<T>(exception);
        }

        public static implicit operator T(Result<T> result)
        {
            if (result.IsSuccess) { return result.Value; }

            var exceptions = new List<Exception>
            {
                new InvalidCastException($"Result cast to {typeof(T).Name} was not successful."),
            };
            exceptions.AddRange(result.Exceptions);

            var aEx = new AggregateException(exceptions);
                
            throw aEx;
        }

        public static implicit operator List<Exception>(Result<T> result)
        {
            return result.Exceptions;
        }
    }

    public sealed class Empty { }
}
