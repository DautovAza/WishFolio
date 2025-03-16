using System.Data;
using Dapper;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Domain.Entities.ReadModels.Friends;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.ReadModels.Users;

namespace WishFolio.Infrastructure.DAL.Read.Repositories;

public class FriendsReadRepository : IFriendsReadRepository
{
    private readonly IDbConnection _connection;

    public FriendsReadRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<PagedCollection<FriendReadModel>> GetUserFrindsAsync(Guid userId, FriendshipStatus friendshipStatus, FilteringInfo filteringInfo, PageInfo pageInfo)
    {
        var filterisgString = string.IsNullOrEmpty(filteringInfo.FilterName) ? "" : "WHERE LOWER(\"Name\") LIKE LOWER(@FilteringName) ";
        var offset = pageInfo.PageSize * (pageInfo.CurrentPageNumber - 1);

        var query = $@"SELECT 
            u.""Id"" AS Id,
            f.""Status"",
            u.""Name"" as Name,
            u.""Email"" AS Email
        FROM ""Friendships"" f
        JOIN ""Users"" u ON f.""FriendId"" = u.""Id""
        WHERE f.""UserId"" = @UserId AND f.""Status"" = @Status
        ORDER BY ""{filteringInfo.OrderBy ?? "Name"}""; ";

        query += filterisgString;

        query += $@"SELECT COUNT(*) AS TotalItems FROM ""Friendships"" 
            {filterisgString}";

        var multi = await _connection.QueryMultipleAsync(query,
           new
           {
               UserId = userId,
               Status = friendshipStatus,
               FilteringName = $"%{filteringInfo.FilterName}%",
               OrderBy = filteringInfo.OrderBy,
               PageSize = pageInfo.PageSize,
               PageNumber = pageInfo.CurrentPageNumber,
               Offse = offset
           });

        var items = await multi.ReadAsync<FriendReadModel>();
        var totalItems = await multi.ReadSingleAsync<int>();

        return new PagedCollection<FriendReadModel>(items, totalItems, pageInfo.CurrentPageNumber, pageInfo.PageSize);
    }
}
