using WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.CancelReservation;

public sealed class CancelReservationItemCommandHandler : ReservationItemCommandHandlerBase<CancelReservationItemCommand>
{
    public CancelReservationItemCommandHandler(ICurrentUserService currentUserService,
        IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork)
        : base(currentUserService, wishListRepository, unitOfWork)
    {
    }

    protected override async Task<Result> HandleReservationCommand(WishlistItem item, User currentUser, CancelReservationItemCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return item.CancelReservation(currentUser);
    }
}
