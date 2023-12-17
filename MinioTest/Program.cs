using Microsoft.OpenApi.Models;
using Minio;
using MinioTest.Model;
using MinioTest.Services.Minio;

var builder = WebApplication.CreateBuilder(args);

Accessor.AppConfiguration = builder.Configuration;

builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer YOUR_TOKEN')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<IMinioService, MinioManager>();
builder.Services.AddControllers();

builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(builder.Configuration["Minio:Client"])
    .WithCredentials(builder.Configuration["Minio:AccessKey"],
        builder.Configuration["Minio:SecretKey"])
    .WithSSL(false));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", " API"); });

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();