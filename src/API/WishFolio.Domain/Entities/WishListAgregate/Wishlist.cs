using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.WishListAgregate;

public class WishList
{
    private readonly List<WishlistItem> _items;

    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public VisabilityLevel Visibility { get; private set; }
    public IReadOnlyList<WishlistItem> Items => _items.AsReadOnly();

    private WishList() { }

    private WishList(Guid ownerId)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        _items = new List<WishlistItem>();
    }

    public Result Update(string name, string description, VisabilityLevel visibility)
    {
        return Result.Combine(SetName(name),
            SetDescription(description),
            SetVisability(visibility));
    }

    public Result AddItem(WishlistItem item)
    {
        if (_items.Any(i => i.Id == item.Id))
        {
            return Result.Failure(DomainErrors.WishListItem.ItemAlreadyExisted(item.Id));
        }

        _items.Add(item);
        return Result.Ok();
    }

    public Result RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            return Result.Failure(DomainErrors.WishListItem.ItemNotFound(itemId));
        }
        _items.Remove(item);
        return Result.Ok();
    }

    private Result SetVisability(VisabilityLevel visibility)
    {
        Visibility = visibility;
        return Result.Ok();
    }

    private Result SetDescription(string description)
    {
        if (description == null)
        {
            return Result.Failure(DomainErrors.WishList.DescriptionIsNull());
        }

        if (description.Length > WishlistInvariants.DescriptionMaxLength)
        {
            return Result.Failure(DomainErrors.WishList.DescriptionIsToLong());
        }

        Description = description;
        return Result.Ok();
    }

    private Result SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(DomainErrors.WishList.NameIsNullOrEmpty());
        }
        if (name.Length > WishlistInvariants.NameMaxLength)
        {
            return Result.Failure(DomainErrors.WishList.NameIsToLong(name));
        }


        Name = name;

        return Result.Ok();
    }

    public static async Task<Result<WishList>> CreateAsync(Guid userId,
        string name,
        string description,
        VisabilityLevel visabilityLevel,
        IWishListRepository wishListRepository)
    {
        if (!await wishListRepository.IsUniqWishListNameForUser(name, userId))
        {
            return Result<WishList>.Failure(DomainErrors.WishList.WishListWithSameNameAlreadyExisted(name));
        }
        var wishList = new WishList(userId);

        return Result<WishList>.Combine(wishList, wishList.SetName(name),
            wishList.SetDescription(description),
            wishList.SetVisability(visabilityLevel));
    }
}
