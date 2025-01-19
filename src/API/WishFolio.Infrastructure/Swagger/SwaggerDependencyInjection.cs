using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace WishFolio.Infrastructure.Swagger;

public static class SwaggerDependencyInjection
{
    public static IServiceCollection AddWishFolioSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "WishFolio API", Version = "v1" });
            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Auth scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()

            }});
        });

        return services;
    }
}