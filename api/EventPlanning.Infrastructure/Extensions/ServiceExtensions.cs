﻿using EventPlanning.Core.Data;
using EventPlanning.Core.Data.Entities;
using EventPlanning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration);
            services.AddUnitOfWork();

            return services;
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
                // TODO: Email confirmation
                // o.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<DatabaseContext>();
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DatabaseContext>(opts =>
            {
                opts.UseSqlServer(configuration["DatabaseConnections:SqlServer"]);
            });
        }
    }
}
