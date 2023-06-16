using System;

namespace Library.Models.Data.Result
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
        public override Exception Exception => null;
    }

}
