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
    private static readonly string[] _fakeNames =
        [
        "Мистер КОТ",
        "Кот чериш",
        "Кот чериш2",
        "Кот чериш3",
        "Sobaka Sytulaya",
        "Случайный прохожий",
        "Просто Боб",
        "Agent не_движимости",
        "Anderson Mr",
        "Persi Val",
        "Виноградом накормлю",
        "Винокур Владимир",
        "Владимир Больших",
        "Влад Брат",
        "Брат Брат",
        "Брат Небрат",
        ];

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
            User.Create("test@test.com", "test user1", 18, "test", passwordHasher).Value!,
            .. CreateTestUsers(2, 10, passwordHasher),
            ..  CreateUsersFromNamesArray(passwordHasher),
        ];

        users[0].AddToFriends(users[1]);
        users[0].AddToFriends(users[2]);
        users[0].AddToFriends(users[3]);
        users[1].AcceptFriendRequest(users[0]);
        users[2].AcceptFriendRequest(users[0]);
        users[3].AcceptFriendRequest(users[0]);
        return users;
    }

    private static async Task<WishList[]> CreateWishLists(WishFolioContext context, User[] users)
    {
        WishListRepository wishListRepository = new WishListRepository(context);
        WishList[] wishLists =
        [
            (await WishList.CreateAsync(users[0].Id, "wishList 1", "wishList 1 description", VisabilityLevel.Public,wishListRepository))!,
            (await WishList.CreateAsync(users[0].Id, "wishList 2", "wishList 2 description", VisabilityLevel.FriendsOnly,wishListRepository))!,
        ];

        WishlistItem[] wishlistItems = CreateItems("wishlist 1", 10);
        foreach (var wishlistItem in wishlistItems)
        {
            _ = wishLists[0].AddItem(wishlistItem);
        }

        return wishLists;
    }

    private static WishlistItem[] CreateItems(string suffix, int count)
    {
        return Enumerable.Range(1, count)
            .Select(number => WishlistItem.Create(string.Join(" ", "item", number, suffix), $"item {number} test description", $"http://somesite/items/item{number}"))
            .Select(e => e.Value)
            .ToArray()!;
    }

    private static User[] CreateTestUsers(int startNumber, int count, IPasswordHasher passwordHasher)
    {
        return Enumerable.Range(startNumber, count)
          .Select(number => User.Create($"test{number}@test.com", $"test user {number}", 18, "test", passwordHasher))
          .Select(e => e.Value)
          .ToArray()!;
    }


    private static User[] CreateUsersFromNamesArray(IPasswordHasher passwordHasher)
    {
        return _fakeNames
          .Select((name, number) => User.Create($"test{number}@test.com", name, 18, "test", passwordHasher))
          .Select(e => e.Value)
          .ToArray()!;
    }

}
