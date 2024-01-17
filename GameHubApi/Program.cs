using Application;
using Common.Helpers;
using GameHubApi;
using GameHubApi.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None;});

// Register Services
builder.Services.AddPresentation()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://127.0.0.1:5173").AllowCredentials());
app.UseOutputCache();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();

// Add endpoint for Logout since it doesn't exist in the Identity Endpoints in .NET 8
app.MapLogOut();
app.MapControllers();

app.Run();
