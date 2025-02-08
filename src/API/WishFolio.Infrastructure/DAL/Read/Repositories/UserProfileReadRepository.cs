using System.Data;
using Dapper;
using WishFolio.Domain.Abstractions.ReadModels.Users;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Infrastructure.DAL.Read.Repositories;

public class UserProfileReadRepository : IUserProfileReadRepository
{
    private readonly IDbConnection _connection;

    public UserProfileReadRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<UserProfileReadModel?> GetByEmail(string email)
    {
        var query = @"SELECT ""Id"", ""Email"", ""Name"", ""Age"" FROM ""Users"" WHERE ""Email"" = @Email";
        return await _connection.QuerySingleOrDefaultAsync<UserProfileReadModel>(query, new { Email = email });
    }

    public async Task<UserProfileReadModel?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT ""Id"", ""Email"", ""Name"", ""Age"" FROM ""Users"" WHERE ""Id"" = @id";
        return await _connection.QuerySingleOrDefaultAsync<UserProfileReadModel>(query, new { Id = id });
    }
}
