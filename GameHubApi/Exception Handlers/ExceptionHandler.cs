using Common.Models;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GameHubApi.Exception_Handlers
{
    public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,"Exception occurred: {Message}",exception.Message);
            var problemDetails = new ProblemDetails();

            if(exception is GameNotFoundException or GamesListNotFoundException or GameScreenshotsNotFoundException or GameTrailersNotFoundException or GenresNotFoundException)
            {
                problemDetails.Title = "Not found error";
                problemDetails.Details = exception.Message;
                problemDetails.StatusCode = HttpStatusCode.NotFound;
            }
            else if(exception is RawgApiException)
            {
                problemDetails.Title = "Api error";
                problemDetails.Details = "An error occured during call to api, please try again."; // I didn't provide server exception details to the client. 
                problemDetails.StatusCode = HttpStatusCode.InternalServerError;
            }else if(exception is HttpRequestException httpException)
            {
                problemDetails.Title = "Http Request error";
                problemDetails.Details = "An error occured during an http request, please try again.";
                problemDetails.StatusCode = httpException.StatusCode ?? HttpStatusCode.InternalServerError;
            }
            else
            {
                problemDetails.Title = "Server error";
                problemDetails.Details = "An error occured in server, please try again.";
                problemDetails.StatusCode = HttpStatusCode.InternalServerError;
            }
            httpContext.Response.StatusCode = (int)problemDetails.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
