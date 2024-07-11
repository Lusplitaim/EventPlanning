using EventPlanning.Core.Models.Options;
using EventPlanning.Core.Services;
using EventPlanning.Core.Storages;
using EventPlanning.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthService = EventPlanning.Core.Services.AuthenticationService;
using IAuthService = EventPlanning.Core.Services.IAuthenticationService;

namespace EventPlanning.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            ConfigureJWT(services, config);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IUserStorage, UserStorage>();
            services.AddScoped<IEventStorage, EventStorage>();

            services.AddScoped<IAuthUtils, AuthUtils>();

            services.Configure<JwtOptions>(config.GetSection(JwtOptions.JwtSettings));

            return services;
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}
