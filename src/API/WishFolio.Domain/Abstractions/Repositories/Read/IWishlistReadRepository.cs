using WishFolio.Domain.Abstractions.ReadModels.WishLlists;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IWishlistReadRepository
{
    Task<IEnumerable<WishlistReadModel>> GetUserWishlistsAsync(Guid userId, VisabilityLevel visabilityLevel);
    Task<WishlistReadModel?> GetUserWishlistsByIdAsync(Guid userId, Guid wishlistId, VisabilityLevel visabilityLevel);
    Task<WishlistReadModel?> GetUserWishlistsByNameAsync(Guid userId, string wishlistName, VisabilityLevel visabilityLevel);
    Task<IEnumerable<WishlistItemReadModel>> GetWishlistItemsAsync(Guid wishListId);
}