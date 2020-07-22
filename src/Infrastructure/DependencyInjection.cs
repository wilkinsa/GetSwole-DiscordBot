using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Application.Common.Interfaces;
using Infrastructure.MemeGenerator;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options
                    .UseMySql("Server=swole-db;Database=GetSwole;User=root;Password=example", mySqlOptions => mySqlOptions
                        .ServerVersion(new Version(8, 0, 20), ServerType.MySql)
                    .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IMemeGenerator, MemeGeneratorService>();
            return services;
        }
    }
}