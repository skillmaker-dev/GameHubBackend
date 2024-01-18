using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GameHubApi.Attributes
{
    public class GetCurrentUserIdAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _logger = context.HttpContext.RequestServices.GetService<ILogger<GetCurrentUserIdAttribute>>();
            _ = _logger ?? throw new Exception("Could not instantiate service for ILogger");

            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                _logger.LogCritical("Could not find current user");
                throw new UserNotFoundException("Could not find current user");
            }
            context.HttpContext.Items["CurrentUserId"] = userId;
        }
    }
}
