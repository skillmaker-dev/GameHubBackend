using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GameHubApi.Attributes
{
    public class GetCurrentUserIdAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.HttpContext.Items["CurrentUserId"] = userId;
        }
    }
}
