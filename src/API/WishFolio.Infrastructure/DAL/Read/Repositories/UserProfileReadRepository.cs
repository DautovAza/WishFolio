using System.Data;
using Dapper;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Domain.Entities.ReadModels.Users;

namespace WishFolio.Infrastructure.DAL.Read.Repositories;

public class UserProfileReadRepository : IUserProfileReadRepository
{
    private readonly IDbConnection _connection;

    public UserProfileReadRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<UserProfileReadModel?> GetByEmailAsync(string email)
    {
        var query = @"SELECT * FROM ""Users"" WHERE ""Id"" = @Id";
        return await _connection.QuerySingleOrDefaultAsync<UserProfileReadModel>(query, new { Email = email });
    }

    public async Task<UserProfileReadModel?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM ""Users"" WHERE ""Id"" = @Id";
        return await _connection.QuerySingleOrDefaultAsync<UserProfileReadModel>(query, new { Id = id });
    }

    public async Task<PagedCollection<UserProfileReadModel>> GetFiltredUsersAsync(string? filteringName, string? orderBy, int pageSize, int pageNumber)
    {
        var filterisgString = string.IsNullOrEmpty(filteringName) ? "" : "WHERE LOWER(\"Name\") LIKE LOWER(@FilteringName)";

        var offset = pageSize * (pageNumber - 1);
        var query = $@"SELECT * 
            FROM ""Users""
            {filterisgString}
            ORDER BY {orderBy ?? "\"Name\""}
            OFFSET @Offse ROWS
            FETCH NEXT @PageSize ROW ONLY; ";

        query += $@"SELECT COUNT(*) AS TotalItems FROM ""Users"" 
            {filterisgString}";

        var multi = await _connection.QueryMultipleAsync(query,
            new
            {
                FilteringName =$"%{filteringName}%",
                OrderBy = orderBy,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Offse = offset
            });

    var items = await multi.ReadAsync<UserProfileReadModel>();
    var totalItems = await multi.ReadSingleAsync<int>();

        return new PagedCollection<UserProfileReadModel>(items, totalItems, pageNumber, pageSize);
    }
}
