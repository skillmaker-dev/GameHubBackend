using Microsoft.AspNetCore.Identity;

namespace GameHubApi.Extensions
{
    public static class IdentityExtensions
    {
        public static WebApplication MapLogOut(this WebApplication application)
        {
            application.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
            {
                await signInManager.SignOutAsync().ConfigureAwait(false);
            }).RequireAuthorization(); // So that only authorized users can use this endpoint

            return application;
        }
    }
}
