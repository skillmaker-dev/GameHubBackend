using System.Net;

namespace Common.Models
{
    public class ProblemDetails
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
