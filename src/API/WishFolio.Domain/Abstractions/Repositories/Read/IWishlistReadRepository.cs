using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Abstractions.ReadModels.WishLlists;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IWishlistReadRepository
{
    Task<PagedCollection<WishlistItemReadModel>> GetPagedWishlistItemsAsync(Guid wishListId, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<WishlistReadModel>> GetUserWishlistsAsync(Guid userId, VisabilityLevel visabilityLevel);
    Task<WishlistReadModel?> GetUserWishlistsByIdAsync(Guid userId, Guid wishlistId, VisabilityLevel visabilityLevel);
    Task<WishlistReadModel?> GetUserWishlistsByNameAsync(Guid userId, string wishlistName, VisabilityLevel visabilityLevel);
    Task<IEnumerable<WishlistItemReadModel>> GetWishlistItemsAsync(Guid wishListId);
}