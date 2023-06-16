using System;

namespace Library.Models.Data.Result
{
    public class Failure<T> : Result<T>
    {
        private readonly Exception exception;

        public Failure(Exception exception)
        {
            this.exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public override bool IsSuccess => false;
        public override T Value => default;
        public override Exception Exception => exception;
    }

}
