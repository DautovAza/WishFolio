using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Wishlists.Commands.UpdateWishList;

public class UpdateWishListCommandHandler : RequestHandlerBase<UpdateWishListCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public override async Task<Result> Handle(UpdateWishListCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var wishList = (await _wishListRepository.GetOwnerWishListsAsync(userId, VisabilityLevel.None))
            .FirstOrDefault();

        if (wishList is null)
        {
            return Failure(DomainErrors.WishList.NotFoundById(request.Id));
        }

        var result = wishList.Update(request.Name, request.Description, request.VisabilityLevel);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
