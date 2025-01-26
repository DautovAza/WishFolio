using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Wishlists.Commands.CreateWishList;

public sealed class CreateWishListCommandHandler : RequestHandlerBase<CreateWishListCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWishListCommandHandler(ICurrentUserService currentUserService, 
        IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _wishListRepository = wishListRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(CreateWishListCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        if (userId == default)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var wishListCreateResult = await WishList.CreateAsync(userId, request.Name, request.Description, request.VisabilityLevel, _wishListRepository);

        if (wishListCreateResult.IsSuccess)
        {
            await _wishListRepository.AddAsync(wishListCreateResult.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return wishListCreateResult;
    }
}


