using Redarbor.Application.Common.Results;
using System.Net;
using System.Text.RegularExpressions;

namespace Redarbor.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails<T>(this Result<T> result, HttpContext context)
    {
        var statusEnum = (HttpStatusCode)result.Code;
        var title = Regex.Replace(statusEnum.ToString(), "(\\B[A-Z])", " $1");

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.Problem(
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    title: title,
                    detail: result.Error,
                    statusCode: result.Code,
                    instance: context.Request.Path
                );
    }
        
}
