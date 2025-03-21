﻿using WishFolio.Application.UseCases.Wishlists.Commands.Items.Abstractions;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Wishlists.Commands.Items.RemoveWishListItem;

public class RemoveWishListItemCommandHandler : WishListItemCommandHandlerBase<RemoveWishListItemCommand>
{
    public RemoveWishListItemCommandHandler(IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
        : base(wishListRepository, unitOfWork, currentUserService)
    {
    }

    protected override async Task<Result> HandleWishListItemCommand(WishList wishList, RemoveWishListItemCommand request, CancellationToken cancellationToken)
    {
        var item = wishList.Items.FirstOrDefault(item => item.Id == request.Id);

        if (item is null)
        {
            return Failure(DomainErrors.WishListItem.ItemNotFound(request.Id));
        }

        await Task.CompletedTask;

        wishList.RemoveItem(request.Id);
        return Ok();
    }
}
