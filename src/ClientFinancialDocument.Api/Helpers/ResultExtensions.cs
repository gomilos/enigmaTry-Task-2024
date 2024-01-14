using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return Results.Problem(
                statusCode: GetResultProblemStatusCode(result.Error.ErrorType),
                title: GetResultProblemTitle(result.Error.ErrorType),
                type: "",
                extensions: new Dictionary<string, object?>
                {
                    {"errors", new[]{result.Error} }
                }
                );
        }

        public static int GetResultProblemStatusCode(ErrorType errorType) =>
               errorType switch
               {
                   ErrorType.Validation => StatusCodes.Status400BadRequest,
                   ErrorType.NotFound => StatusCodes.Status404NotFound,
                   ErrorType.Forbiden => StatusCodes.Status403Forbidden,
                   _ => StatusCodes.Status500InternalServerError,
               };
        static string GetResultProblemTitle(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "Bad Request",
                    ErrorType.NotFound => "Not Found",
                    ErrorType.Forbiden => "Forbidden",
                    _ => "Internal Server Error",
                };
    }
}
