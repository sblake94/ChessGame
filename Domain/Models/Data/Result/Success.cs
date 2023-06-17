using System;
using System.Collections.Generic;

namespace Domain.Models.Data.Result
{
    public class Success<T> : Result<T>
    {
        private readonly T value;

        public Success(T value)
        {
            this.value = value;
        }

        public override bool IsSuccess => true;
        public override T Value => value;
        public override List<Exception> Exceptions => null;
        public override AggregateException ThrowableException => new AggregateException(Exceptions);
    }

}
