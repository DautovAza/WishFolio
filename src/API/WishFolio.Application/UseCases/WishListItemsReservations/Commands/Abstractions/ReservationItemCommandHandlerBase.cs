using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;

public abstract class ReservationItemCommandHandlerBase<TCommand> : RequestHandlerBase<TCommand>
    where TCommand : RequestBase, IReservationItemCommand
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;

    protected ReservationItemCommandHandlerBase(ICurrentUserService currentUserService,
        IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _wishListRepository = wishListRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        var item = await _wishListRepository.GetWishListItemByIdAsync(request.ItemId);

        if (item is null)
        {
            return Result.Failure(DomainErrors.WishListItem.ItemNotFound(request.ItemId));
        }

        var result = await HandleReservationCommand(item, currentUser, request, cancellationToken);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    protected abstract Task<Result> HandleReservationCommand(WishlistItem item, User currentUser, TCommand request, CancellationToken cancellationToken);
}
