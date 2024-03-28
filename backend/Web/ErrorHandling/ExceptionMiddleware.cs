using System.Net;
using Backend.Infrastructure.Exceptions;

namespace Backend.Web.ErrorHandling;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            HttpStatusCode status;
            var errorMesage = exception.Message;

            switch (exception)
            {
                case DbIntegrityException:
                    status = HttpStatusCode.UnprocessableEntity;
                    break;
                case DbNotFoundException:
                    status = HttpStatusCode.NotFound;
                    break;
                case NullReferenceException:
                    status = HttpStatusCode.BadRequest;
                    errorMesage = "Query parameters can't be empty";
                    break;
                case ArgumentException:
                    status = HttpStatusCode.BadRequest;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsJsonAsync(
                new ErrorResult
                {
                    Status = status,
                    Errors = new Dictionary<string, string> { ["*"] = errorMesage }
                }
            );
        }
    }
}