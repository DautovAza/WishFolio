using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Wishlists.Commands.Items.Abstractions;

public abstract class WishListItemCommandHandlerBase<TCommand> : RequestHandlerBase<TCommand>
    where TCommand : RequestBase, IWishListItemCommand
{
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    protected WishListItemCommandHandlerBase(IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _wishListRepository = wishListRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public override async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, request.WishListName, VisabilityLevel.None);

        if (wishlist is null)
        {
            return Result.Failure(DomainErrors.WishList.NotFoundByName(request.WishListName));
        }

        var result = await HandleWishListItemCommand(wishlist, request, cancellationToken);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }

    protected abstract Task<Result> HandleWishListItemCommand(WishList wishList, TCommand request, CancellationToken cancellationToken);
}
