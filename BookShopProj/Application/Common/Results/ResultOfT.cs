using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Results
{

    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Cannot access value of a failed result.");

        private Result(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public static Result<T> Success(T value) =>
            new(value, true, Error.None);

        public new static Result<T> Failure(Error error) =>
            new(default, false, error);
    }

}
