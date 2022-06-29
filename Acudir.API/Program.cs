using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

// Add services to the container.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
WebApplication app = builder.Build();

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

app.UseApiVersioning();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();