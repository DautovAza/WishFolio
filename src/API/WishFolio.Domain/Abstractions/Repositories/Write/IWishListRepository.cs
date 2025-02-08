using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Abstractions.Repositories.Write;

public interface IWishListRepository
{
    Task AddAsync(WishList wishlist);
    Task<bool> IsUniqWishListNameForUser(string name, Guid userId);
    Task<IEnumerable<WishList>> GetOwnerWishListsAsync(Guid ownerId, VisabilityLevel visabilityLevel);
    Task<WishList?> GetOwnerWishListByNameAsync(Guid userId, string name, VisabilityLevel visabilityLevel);
    Task<WishlistItem?> GetWishListItemByIdAsync(Guid itemId);
    Task RemoveAsync(WishList wishlist);
}
