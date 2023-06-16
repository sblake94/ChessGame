using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Data.Result
{
    public class Invalid<T> : Result<T>
    {
        private readonly T value;
        private readonly Exception exception;

        public Invalid(T value, Exception exception)
        {
            this.value = value;
            this.exception = exception;
        }

        public override bool IsSuccess => false;
        public override T Value => value;
        public override Exception Exception => exception;


    }
}