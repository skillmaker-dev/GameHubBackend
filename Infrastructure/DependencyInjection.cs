using Application.Services.Email;
using Application.Services.FavoriteGames;
using Application.Services.HttpClient;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.External_Services.RAWG;
using Infrastructure.Identity;
using Infrastructure.Services.Games;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("AppDb"));
            services.AddAuthorization();

            services.AddIdentityApiEndpoints<ApplicationUser>(opt => 
                                                            { 
                                                                opt.Password.RequiredLength = 8; 
                                                                opt.User.RequireUniqueEmail = true; 
                                                                opt.Password.RequireNonAlphanumeric = false;
                                                                opt.SignIn.RequireConfirmedEmail = false; 
                                                            })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddFluentEmail(configuration);

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddHttpClient<IRawgApiClient, RawgApiClient>();
            services.AddScoped<IGamesService, GamesService>();

            return services;
        }
    }
}
