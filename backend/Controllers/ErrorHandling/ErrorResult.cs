using System.Net;

namespace backend.Controllers.ErrorHandling
{
    public class ErrorResult
    {
        public required HttpStatusCode Status { get; set; }

        public required Dictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}