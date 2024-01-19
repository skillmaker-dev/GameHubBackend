using GameHubApi.Exception_Handlers;
using Microsoft.Extensions.Options;

namespace GameHubApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddCors();
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
            services.AddOutputCache(options => options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromHours(12))));
            return services;
        }
    }
}
