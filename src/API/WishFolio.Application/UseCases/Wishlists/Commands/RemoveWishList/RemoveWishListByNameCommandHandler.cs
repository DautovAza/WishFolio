using WishFolio.Application.Common;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Wishlists.Commands.RemoveWishList;

public class RemoveWishListByNameCommandHandler : RequestHandlerBase<RemoveWishListByNameCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public override async Task<Result> Handle(RemoveWishListByNameCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, request.Name, VisabilityLevel.None);

        if (wishlist is null)
        {
            return Result.Failure(DomainErrors.WishList.NotFoundByName(request.Name));
        }

        await _wishListRepository.RemoveAsync(wishlist);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
