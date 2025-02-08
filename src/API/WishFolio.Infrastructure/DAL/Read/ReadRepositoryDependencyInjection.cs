using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Infrastructure.DAL.Read.Repositories;

namespace WishFolio.Infrastructure.DAL.Read;

public static class ReadRepositoryDependencyInjection
{
    public static IServiceCollection AddReadRepositoriesWithDapper(this IServiceCollection serives, IConfiguration configuration)
    {
        serives.AddScoped((Func<IServiceProvider, IDbConnection>)(s =>
        {
            var connectionString = configuration.GetConnectionString("PostgreConnection");
            var npgsqlConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreConnection"));
            npgsqlConnection.Open();
            return npgsqlConnection;

        }));

        serives
            .AddScoped<IUserProfileReadRepository, UserProfileReadRepository>();

        return serives;
    }
}
