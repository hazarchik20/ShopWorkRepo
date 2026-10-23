using Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookShopProj.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(
            this Result result)
        {
            if (result.IsSuccess)
                return new NoContentResult();

            return CreateErrorResult(result.Error);
        }

        public static IActionResult ToActionResult<T>(
            this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Value);

            return CreateErrorResult(result.Error);
        }

        private static IActionResult CreateErrorResult(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => 400,
                ErrorType.BadRequest => 400,
                ErrorType.Unauthorized => 401,
                ErrorType.Forbidden => 403,
                ErrorType.NotFound => 404,
                ErrorType.Conflict => 409,
                _ => 500
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = error.Code,
                Detail = statusCode == 500
                    ? "Внутрішня помилка сервера."
                    : error.Message
            };

            problem.Extensions["code"] = error.Code;

            return new ObjectResult(problem)
            {
                StatusCode = statusCode
            };
        }
    }
}
