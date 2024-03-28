using System.Net;

namespace Backend.Web.ErrorHandling;

public struct ErrorResult
{
    public required HttpStatusCode Status { get; set; }

    public required Dictionary<string, string> Errors { get; set; }
}