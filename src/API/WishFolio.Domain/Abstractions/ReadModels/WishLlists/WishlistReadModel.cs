using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Domain.Abstractions.ReadModels.WishLlists;

public class WishlistReadModel
{
    public Guid Id { get;  set; }
    public Guid OwnerId { get;  set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public VisabilityLevel Visibility { get;  set; }
}

public class WishlistItemReadModel
{
    public Guid Id { get;  set; }
    public Guid WishListId { get;  set; }
    public string Name { get;  set; }
    public string Description { get; set; }
    public string? Uri { get; set; }

    public Guid? ReservationUserId { get; set; }
    public ReservationStatus ReservationStatus { get; set; }
}