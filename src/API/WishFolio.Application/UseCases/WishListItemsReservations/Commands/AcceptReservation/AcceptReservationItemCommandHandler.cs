using WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.AcceptReservation;

public sealed class AcceptReservationItemCommandHandler : ReservationItemCommandHandlerBase<AcceptReservationItemCommand>
{
    public AcceptReservationItemCommandHandler(ICurrentUserService currentUserService,
        IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork)
        : base(currentUserService, wishListRepository, unitOfWork)
    {
    }

    protected override async Task<Result> HandleReservationCommand(WishlistItem item, User currentUser, AcceptReservationItemCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return item.CloseReservation(currentUser);
    }
}