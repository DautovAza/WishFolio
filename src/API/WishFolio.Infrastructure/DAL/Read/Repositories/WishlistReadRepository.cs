using System.Data;
using Dapper;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Domain.Abstractions.ReadModels.WishLlists;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Infrastructure.DAL.Read.Repositories;

public class WishlistReadRepository : IWishlistReadRepository
{
    private readonly IDbConnection _connection;

    public WishlistReadRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<IEnumerable<WishlistReadModel>> GetUserWishlistsAsync(Guid userId, VisabilityLevel visabilityLevel)
    {
        var query = @"SELECT * FROM ""Wishlists"" WHERE ""OwnerId"" = @UserId AND ""Visibility"" <= @VisabilityLevel";
        return await _connection.QueryAsync<WishlistReadModel>(query, new { UserId = userId, VisabilityLevel = visabilityLevel });

    }

    public async Task<WishlistReadModel?> GetUserWishlistsByIdAsync(Guid userId, Guid wishlistId, VisabilityLevel visabilityLevel)
    {
        var query =
            @"SELECT * 
            FROM ""Wishlists"" 
            WHERE ""Id"" = @Id  
            AND ""OwnerId"" = @UserId  
            AND ""Visibility"" <= @VisabilityLevel";

        return await _connection.QuerySingleOrDefaultAsync<WishlistReadModel>(
            query,
            new
            {
                Id = wishlistId,
                UserId = userId,
                VisabilityLevel = visabilityLevel
            });
    }

    public async Task<WishlistReadModel?> GetUserWishlistsByNameAsync(Guid userId, string wishlistName, VisabilityLevel visabilityLevel)
    {
        var query =
            @"SELECT * 
            FROM ""Wishlists"" 
            WHERE ""Name"" = @WishlistName
            AND ""OwnerId"" = @UserId  
            AND ""Visibility"" <= @VisabilityLevel";

        return await _connection.QuerySingleOrDefaultAsync<WishlistReadModel>(
            query,
            new
            {
                WishlistName = wishlistName,
                UserId = userId,
                VisabilityLevel = visabilityLevel
            });
    }

    public async Task<IEnumerable<WishlistItemReadModel>> GetWishlistItemsAsync(Guid wishListId)
    {
        var query = @"SELECT * 
            FROM ""WishlistItems"" 
            WHERE ""WishListId"" = @WishListId";

        return await _connection.QueryAsync<WishlistItemReadModel>(query, new { WishListId = wishListId });
    }

    public async Task<PagedCollection<WishlistItemReadModel>> GetPagedWishlistItemsAsync(Guid wishListId, int pageNumber = 1, int pageSize = 10)
    {
        var offset = pageSize * (pageNumber - 1);
        var query = @"SELECT * 
            FROM ""WishlistItems"" 
            WHERE ""WishListId"" = @WishListId
            ORDER BY ""Name""
            OFFSET @Offse ROWS
            FETCH NEXT @PageSize ROW ONLY; ";
        query += @"SELECT COUNT(*) AS TotalItems FROM ""WishlistItems"" 
            WHERE ""WishListId"" = @WishListId";

        var multi = await _connection.QueryMultipleAsync(query,
            new
            {
                WishListId = wishListId,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Offse = offset
            });

        var items = await multi.ReadAsync<WishlistItemReadModel>();
        var totalItems = await multi.ReadSingleAsync<int>();

        return new PagedCollection<WishlistItemReadModel>(items, totalItems, pageNumber, pageSize);
    }
}