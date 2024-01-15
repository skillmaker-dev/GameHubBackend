using System.Net;

namespace Infrastructure.Exceptions
{
    public class RawgApiException(string message, HttpStatusCode statusCode) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; set; } = statusCode;
    }
}
