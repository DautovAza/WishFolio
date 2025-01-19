using WishFolio.Application.UseCases.Wishlists.Dtos;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists;
public interface IWishListsService
{
    Task<List<WishListDto>> GetAllUserWishListsAsync(Guid userId);
    Task<WishListItemDetailsDto> GetWishListItemDetailAsync(Guid userId, string wishListName, Guid itemId);

    Task AddWishListAsync(string name, string description, VisabilityLevel visabilityLevel);
    Task AddWishListItemAsync(string wishListName, string name, string description, string linkUrl);
    Task RemoveWishListByNameAsync(string name);
    Task RemoveWishListItemAsync(string wishListName, Guid itemId);
    Task UpdateWishListItemAsync(string wishListName, Guid itemId, string name, string description, string linkUrl);
}