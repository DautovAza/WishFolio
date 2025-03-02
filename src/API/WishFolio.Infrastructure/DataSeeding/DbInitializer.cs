using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Infrastructure.Dal.Write;
using WishFolio.Infrastructure.Dal.Write.Repositories;

namespace WishFolio.Infrastructure.DataSeeding;

public static class DbInitializer
{
    public static async Task<IHost> UseTestDataAsync(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<WishFolioContext>();
        var passwordHasher = scope.ServiceProvider.GetService<IPasswordHasher>();

        await context.AddTestDataAsync(passwordHasher);
        return app;
    }

    private static async Task AddTestDataAsync(this WishFolioContext context, IPasswordHasher passwordHasher)
    {
        if (context.Users.Any())
        {
            return;
        }
        var users = CreateUsers(passwordHasher);
        var wishlists = await CreateWishLists(context, users);

        await context.Users.AddRangeAsync(users);
        await context.WishLists.AddRangeAsync(wishlists);

        await context.SaveChangesAsync();
    }

    private static User[] CreateUsers(IPasswordHasher passwordHasher)
    {
        User[] users =
        [
            User.Create("test@test.com", "test user1", 18, "test", passwordHasher).Value,
            User.Create("test2@test.com", "test user2", 18, "test", passwordHasher).Value,
            User.Create("test3@test.com", "test user3", 18, "test", passwordHasher).Value,
            User.Create("test4@test.com", "test user4", 18, "test", passwordHasher).Value,
        ];

        users[0].AddToFriends(users[1]);
        users[1].AcceptFriendRequest(users[0]);
        return users;
    }

    private static async Task<WishList[]> CreateWishLists(WishFolioContext context, User[] users)
    {
        WishListRepository wishListRepository = new WishListRepository(context);
        WishList[] wishLists =
        [
            await WishList.CreateAsync(users[0].Id, "wishList 1", "wishList 1 description", VisabilityLevel.Public,wishListRepository),
            await WishList.CreateAsync(users[0].Id, "wishList 2", "wishList 2 description", VisabilityLevel.FriendsOnly,wishListRepository),
        ];

        WishlistItem[] wishlistItems =
        [
            WishlistItem.Create("item 1", " item 1 test description. Owned by wishlist 1", "http://somesite/items/alotofmoney").Value,
            WishlistItem.Create("item 2", " item 2 test description. Owned by wishlist 1","http://somesite/items/rabbit").Value,
            WishlistItem.Create("item 3", " item 3 test description. Owned by wishlist 1","http://somesite/items/item3").Value,
            WishlistItem.Create("item 4", " item 4 test description. Owned by wishlist 1","http://somesite/items/item4").Value,
            WishlistItem.Create("item 5", " item 5 test description. Owned by wishlist 1","http://somesite/items/item5").Value,
            WishlistItem.Create("item 6", " item 6 test description. Owned by wishlist 1","http://somesite/items/item6").Value,
            WishlistItem.Create("item 7", " item 7 test description. Owned by wishlist 1","http://somesite/items/item7").Value,
            WishlistItem.Create("item 8", " item 8 test description. Owned by wishlist 1","http://somesite/items/item8").Value,
            WishlistItem.Create("item 9", " item 9 test description. Owned by wishlist 1","http://somesite/items/item9").Value,
            WishlistItem.Create("item 10", " item 10 test description. Owned by wishlist 1","http://somesite/items/item10").Value,
        ];

        foreach (var wishlistItem in wishlistItems)
        {
            _ = wishLists[0].AddItem(wishlistItem);
        }

        return wishLists;
    }
}
