using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Data.Result
{
    public class SuccessWithErrors<T> : Result<T>
    {
        private readonly T value;

        public SuccessWithErrors(T value, List<Exception> exceptions)
        {
            this.value = value;
        }

        public override bool IsSuccess => true;
        public override T Value => value;
        public override List<Exception> Exceptions => null;
        public override AggregateException ThrowableException => new AggregateException(Exceptions);
    }
}
