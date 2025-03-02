using Microsoft.EntityFrameworkCore;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Infrastructure.Dal.Write.Repositories;

public class WishListRepository : IWishListRepository
{
    private readonly WishFolioContext _context;

    public WishListRepository(WishFolioContext context)
    {
        _context = context;
    }

    public async Task AddAsync(WishList wishlist)
    {
        await _context.AddAsync(wishlist);
    }

    public async Task<WishList?> GetOwnerWishListByNameAsync(Guid userId, string name, VisabilityLevel visabilityLevel)
    {
        return await _context.WishLists
            .Where(wl => wl.OwnerId == userId)
            .Where(wl => wl.Visibility <= visabilityLevel)
            .FirstOrDefaultAsync(wl => wl.Name == name);
    }

    public async Task<IEnumerable<WishList>> GetOwnerWishListsAsync(Guid ownerId, VisabilityLevel visabilityLevel)
    {
        return await _context.WishLists
             .Where(wl => wl.OwnerId == ownerId)
             .Where(wl => wl.Visibility <= visabilityLevel)
             .ToListAsync();
    }

    public async Task<WishlistItem?> GetWishListItemByIdAsync(Guid itemId)
    {
        return await _context.WishListItems.FirstOrDefaultAsync(wl => wl.Id == itemId);
    }

    public async Task<bool> IsUniqWishListNameForUser(string name, Guid userId)
    {
        return await _context.WishLists
            .Where(wl => wl.OwnerId == userId)
            .AllAsync(wl => wl.Name != name);
    }

    public async Task RemoveAsync(WishList wishlist)
    {
        wishlist = await _context.WishLists.FirstOrDefaultAsync(wl => wl.Id == wishlist.Id);
        _context.WishLists.Remove(wishlist);
        await Task.CompletedTask;
    }
}