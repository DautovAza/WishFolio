using WishFolio.Application.Common;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.AcceptReservation;

public record AcceptReservationItemCommand(Guid ItemId)
    : RequestBase, IReservationItemCommand;
