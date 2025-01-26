using WishFolio.Application.UseCases.WishListItems.Commands.Abstractions;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.WishListItems.Commands.AddWishListItem;

public sealed class AddItemToWishListCommandHandler : WishListItemCommandHandlerBase<AddItemToWishListCommand>
{
    public AddItemToWishListCommandHandler(IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
        : base(wishListRepository, unitOfWork, currentUserService)
    {
    }

    protected override async Task<Result> HandleWishListItemCommand(WishList wishList, AddItemToWishListCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var itemCreateResult = WishlistItem.Create(request.Name, request.Description, request.LinkUrl);

        if (itemCreateResult.IsFailure)
        {
            return itemCreateResult;
        }
        return wishList.AddItem(itemCreateResult.Value);
    }
}
