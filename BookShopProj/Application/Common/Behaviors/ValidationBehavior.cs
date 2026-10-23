using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{


    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            foreach (var validator in _validators)
            {
                var context = new ValidationContext<TRequest>(request);

                var result = await validator.ValidateAsync(
                    context,
                    cancellationToken);

                errors.AddRange(
                    result.Errors.Select(x => x.ErrorMessage));
            }

            if (errors.Count == 0)
            {
                return await next(cancellationToken);
            }

            var error = Error.Validation(
                "Validation.Failed",
                string.Join("; ", errors));

            if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(error);
            }

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition()
                    == typeof(Result<>))
            {
                var resultType = typeof(TResponse);

                var failureMethod = resultType.GetMethod(
                    "Failure",
                    new[] { typeof(Error) });

                if (failureMethod is not null)
                {
                    return (TResponse)failureMethod.Invoke(
                        null,
                        new object[] { error })!;
                }
            }

            throw new InvalidOperationException(
                $"Unsupported response type: {typeof(TResponse).Name}");
        }
    }

}
