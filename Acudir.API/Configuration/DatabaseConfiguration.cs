using System;
using Acudir.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Acudir.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("AcudirChallengeDatabase");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            return services;
        }

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app, IServiceProvider serviceProvider, IHostEnvironment environment)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (environment.IsDevelopment())
                    context.Database.Migrate();
            }

            return app;
        }
    }
}

