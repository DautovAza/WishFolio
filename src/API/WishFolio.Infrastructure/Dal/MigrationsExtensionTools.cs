using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WishFolio.Infrastructure.Dal;

public static class MigrationsExtensionTools
{
    public static IHost ApplyMigrations(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<WishFolioContext>();
            db.Database.Migrate();
        }
        return host;
    }
}