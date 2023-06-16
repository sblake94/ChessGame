using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Data.Result
{
    public class CriticalError<T> : Result<T>
    {
        private readonly Exception exception;

        public CriticalError(Exception exception)
        {
            this.exception = exception ?? throw new ArgumentNullException(nameof(exception));

            throw exception;
        }

        public override bool IsSuccess => false;
        public override T Value => default;
        public override Exception Exception => exception;
    }
}
