using Acudir.API.Configuration;
using Acudir.Infrastructure;
using Acudir.Services;
using Acudir.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


// Add services to the container.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Formatting = Formatting.Indented;
    });
builder.Services.AddEndpointsApiExplorer();

// api versioning
builder.Services
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

// database
builder.Services.AddDatabaseModule(builder.Configuration);
builder.Services
    .AddTransient<IPersonasService, PersonasService>();

builder.Services
    .AddRouting(opt => opt.LowercaseUrls = true);

// swagger documentation
builder.Services.AddSwaggerGen(setup =>
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

// Configure the HTTP request pipeline
WebApplication app = builder.Build();
IServiceProvider serviceProvider = app.Services.GetRequiredService<IServiceProvider>();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI(setup =>
        {
            setup.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
            setup.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        });
}
//app.UseDatabaseMigration(serviceProvider, app.Environment);
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (builder.Environment.IsDevelopment())
        context.Database.Migrate();
}

app.UseApiVersioning();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(policy =>
{
    policy
        .WithOrigins("*")
        .WithMethods("*")
        .WithHeaders("*")
        .WithExposedHeaders("Authorization,Link,X-Total-Count,X-Pagination")
        .SetPreflightMaxAge(TimeSpan.FromSeconds(1800));
});
app.MapControllers();
app.Run();