using System.ComponentModel.DataAnnotations;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate.ValueObjects;

namespace WishFolio.Domain.Entities.WishListAgregate;

public class WishlistItem : AuditableEntity
{
    [Required]
    public Guid Id { get; private set; }

    [Required]
    [MinLength(WishlistItemInvariants.NameMinLength)]
    [MaxLength(WishlistItemInvariants.NameMaxLength)]
    public string Name { get; private set; }

    [MaxLength(WishlistItemInvariants.DescriptionMaxLength)]
    public string Description { get; private set; }

    public WishItemLink Link { get; private set; }

    public Guid? ReservationUserId { get; private set; }
    public ReservationStatus ReservationStatus { get; private set; }

    private WishlistItem() : base() { }

    public WishlistItem(string name, string description, string uri)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название элемента не может быть пустым.");
        }

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Link = new WishItemLink(uri);
    }

    public void Update(string name, string description, string link)
    {
        if (ReservationStatus != ReservationStatus.Available)
        {
            throw new Exception("Нельзя изменить параметры, т.к. это объект забронирован другим пользователем!");
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        if (description != null)
        {
            Description = description;
        }
        if (!string.IsNullOrWhiteSpace(link))
        {
            Link = new WishItemLink(link);
        }
    }

    public void ReserveItem(User user, bool isAnonymous)
    {
        if (ReservationStatus != ReservationStatus.Available)
        {
            throw new Exception("Нельзя забронирован, т.к. это объект забронирован другим пользователем!");
        }

        ReservationStatus = isAnonymous ? ReservationStatus.ReservedAnonymous : ReservationStatus.Reserved;
        ReservationUserId = user.Id;
    }

    public void CancelReservation(User user)
    {
        if (ReservationStatus != ReservationStatus.Reserved || user.Id != ReservationUserId)
        {
            throw new Exception("Нельзя снять бронь, т.к. это объект зарезервирован другим пользователем!");
        }

        ReservationStatus = ReservationStatus.Available;
        ReservationUserId = user.Id;
    }

    public void CloseReservation(User user)
    {
        if (ReservationStatus != ReservationStatus.Reserved || user.Id != ReservationUserId)
        {
            throw new Exception("Нельзя подтверддить бронь, т.к. это объект зарезервирован другим пользователем!");
        }

        ReservationStatus = ReservationStatus.Clsoed;
    }
}