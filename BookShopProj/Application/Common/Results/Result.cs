using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Results
{

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new ArgumentException(
                    "Successful result cannot contain an error.");

            if (!isSuccess && error == Error.None)
                throw new ArgumentException(
                    "Failed result must contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() =>
            new(true, Error.None);

        public static Result Failure(Error error) =>
            new(false, error);

        public static Result<T> Success<T>(T value) =>
            Result<T>.Success(value);

        public static Result<T> Failure<T>(Error error) =>
            Result<T>.Failure(error);
    }

}
