using Microsoft.AspNetCore.Builder;

namespace WishFolio.Infrastructure.CORS;

public static class ConfigureCorsExtension
{
    public static IApplicationBuilder ConfigureCors(this IApplicationBuilder application)
    {
        return application.UseCors(options =>
             {
                 options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
             });
    }
}
