using System.Data;
using Dapper;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Abstractions.ReadModels.Friends;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Infrastructure.DAL.Read.Repositories;

public class FriendsReadRepository : IFriendsReadRepository
{
    private readonly IDbConnection _connection;

    public FriendsReadRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<FriendReadModel>> GetUserFriendsAsync(Guid userId, FriendshipStatus friendshipStatus)
    {
        var query = @"SELECT 
            u.""Id"" AS Id,
            f.""Status"",
            u.""Name"",
            u.""Email"" AS Email
        FROM ""Friendships"" f
        JOIN ""Users"" u ON f.""FriendId"" = u.""Id""
        WHERE f.""UserId"" = @UserId AND f.""Status"" = @Status";
        return await _connection.QueryAsync<FriendReadModel>(query, new { UserId = userId ,Status = friendshipStatus });
    }
}
