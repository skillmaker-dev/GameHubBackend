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

            if(exception is GameNotFoundException or GamesListNotFoundException)
            {
                problemDetails.Title = "Not found";
                problemDetails.Details = exception.Message;
                problemDetails.StatusCode = HttpStatusCode.NotFound;
            }

            httpContext.Response.StatusCode = (int)problemDetails.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
