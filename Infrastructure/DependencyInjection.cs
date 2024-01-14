using Application.Interfaces.Email;
using Infrastructure.Data;
using Infrastructure.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("AppDb"));
            services.AddAuthorization();

            services.AddIdentityApiEndpoints<IdentityUser>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddFluentEmail(configuration);

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
