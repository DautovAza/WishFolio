using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.CancelReservation;

public record CancelReservationItemCommand(Guid ItemId)
    : RequestBase, IReservationItemCommand;
