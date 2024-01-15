using GameHubApi.Exception_Handlers;

namespace GameHubApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) 
        { 
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
            services.AddOutputCache(opt => opt.DefaultExpirationTimeSpan = TimeSpan.FromDays(1));
            return services; 
        }
    }
}
