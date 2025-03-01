using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate.ValueObjects;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.WishListAgregate;

public class WishlistItem : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public WishItemLink? Link { get; private set; }

    public Guid? ReservationUserId { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; }

    private WishlistItem() : base() { }

    private WishlistItem(Guid id)
    {
        Id = id;
        ReservationStatus = ReservationStatus.Available;
    }

    public Result Update(string name, string description, string link)
    {
        if (ReservationStatus != ReservationStatus.Available)
        {
            throw new Exception("Нельзя изменить параметры, т.к. это объект забронирован другим пользователем!");
        }

        return Result.Combine(SetName(name),
            SetDescription(description),
            SetLink(link));
    }

    private Result SetLink(string uri)
    {
        var createLinkResult = WishItemLink.Create(uri);

        if (createLinkResult.IsSuccess)
        {
            Link = Link;
        }

        return createLinkResult;
    }

    public Result ReserveItem(User user, bool isAnonymous)
    {
        if (ReservationStatus != ReservationStatus.Available)
        {
            return Result.Failure(DomainErrors.WishListItem.ItemAlreadyReserved(Name));
        }

        ReservationStatus = isAnonymous ? ReservationStatus.ReservedAnonymous : ReservationStatus.Reserved;
        ReservationUserId = user.Id;

        return Result.Ok();
    }

    public Result CancelReservation(User user)
    {
        if (ReservationStatus != ReservationStatus.Reserved || user.Id != ReservationUserId)
        {
            return Result.Failure(DomainErrors.WishListItem.ItemAlreadyReservedByOtherUser(Name));
        }

        ReservationStatus = ReservationStatus.Available;
        ReservationUserId = user.Id;

        return Result.Ok();
    }

    public Result CloseReservation(User user)
    {
        if (ReservationStatus != ReservationStatus.Reserved || user.Id != ReservationUserId)
        {
            return Result.Failure(DomainErrors.WishListItem.ItemAlreadyReservedByOtherUser(Name));
        }

        ReservationStatus = ReservationStatus.Clsoed;

        return Result.Ok();
    }

    private Result SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure(DomainErrors.WishListItem.NameIsNullOrEmpty());
        }

        if (name.Length > WishlistItemInvariants.NameMaxLength)
        {
            return Result.Failure(DomainErrors.WishListItem.NameIsToLong(name));
        }

        if (name.Length < WishlistItemInvariants.NameMinLength)
        {
            return Result.Failure(DomainErrors.WishListItem.NameIsToShort(name));
        }

        Name = name;

        return Result.Ok();
    }

    private Result SetDescription(string description)
    {
        if (description is null)
        {
            return Result.Failure(DomainErrors.WishListItem.DescriptionIsNull());
        }

        if (description.Length > WishlistItemInvariants.DescriptionMaxLength)
        {
            return Result.Failure(DomainErrors.WishListItem.DescriptionIsToLong());
        }

        Description = description;

        return Result.Ok();
    }

    public static Result<WishlistItem> Create(string name, string description, string link)
    {
        var item = new WishlistItem(Guid.NewGuid());
        var result = item.Update(name, description, link);

        return Result<WishlistItem>.Combine(item, result);
    }
}