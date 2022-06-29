using Acudir.API.Configuration;
using Acudir.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

[assembly: ApiController]

namespace Acudir.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseModule(_configuration);

            services.AddSwaggerGen(setup =>
            {
                OpenApiContact openApiContact = new OpenApiContact()
                {
                    Name = "Acudir Emergencias",
                    Email = "arquitectura@acudiremergencias.com.ar"
                };

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Bearer Token Authorization.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Acudir Challenge API",
                    Version = "1.0",
                    Description = "Contrato Acudir Challenge REST API",
                    Contact = openApiContact
                });
                setup.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Title = "Acudir Challenge API",
                    Version = "2.0",
                    Description = "Contrato Acudir Challenge REST API",
                    Contact = openApiContact
                });
            });

            services
                .AddApiVersioning(setup =>
                {
                    setup.AssumeDefaultVersionWhenUnspecified = true;
                    setup.DefaultApiVersion = new ApiVersion(1, 0);
                    setup.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddAuthentication();
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            IdentityModelEventSource.ShowPII = true;

            if(_environment.IsDevelopment())
            {
                app
                    .UseSwagger()
                    .UseSwaggerUI(setup =>
                    {
                        setup.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                        setup.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                    });
            }

            app
                .UseDatabaseMigration(serviceProvider, env)
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                })
                .UseApiVersioning()
                .UseHttpsRedirection();
        }
    }
}