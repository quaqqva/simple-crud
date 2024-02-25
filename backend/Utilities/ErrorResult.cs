using System.Net;

namespace backend.Utilities
{
    public struct ErrorResult
    {
        public required HttpStatusCode Status { get; set; }

        public required Dictionary<string, string> Errors { get; set; }
    }
}
