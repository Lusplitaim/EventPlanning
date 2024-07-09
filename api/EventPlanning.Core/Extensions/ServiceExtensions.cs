using EventPlanning.Core.Services;
using EventPlanning.Core.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUserStorage, UserStorage>();

            return services;
        }
    }
}
