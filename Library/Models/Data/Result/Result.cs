using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace Library.Models.Data.Result
{
    public abstract class Result<T>
    {
        public abstract bool IsSuccess { get; }
        public abstract T Value { get; }
        public abstract Exception Exception { get; }

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
            return result.IsSuccess ? result.Value : throw result.Exception;
        }

        public static implicit operator Exception(Result<T> result)
        {
            return result.Exception;
        }
    }

    public sealed class Empty { }
}
