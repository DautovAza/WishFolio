using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.ReserveItem;

public record ReserveItemCommand(Guid ItemId, bool IsAnonymous)
    : RequestBase, IReservationItemCommand;
