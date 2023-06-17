using System;
using System.Collections.Generic;

namespace Domain.Models.Data.Result
{
    public class Failure<T> : Result<T>
    {
        public Failure(Exception exception)
        {
            Exceptions.Add(exception);
        }

        public override bool IsSuccess => false;
        public override T Value => default;
        public override List<Exception> Exceptions { get; } = new List<Exception>();
        public override AggregateException ThrowableException => new AggregateException(Exceptions);
    }

}
