using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.WebApi.Controllers.WishLists.Dtos;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Application.UseCases.WishListItems.Commands.AddWishListItem;
using WishFolio.Application.UseCases.WishListItems.Commands.RemoveWishListItem;
using WishFolio.Application.UseCases.WishListItems.Commands.UpdateWishListItem;
using WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/WishLists/{wishListName}/[controller]")]
public class WishListsItemsManagmentController : ResultHandlerControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public WishListsItemsManagmentController(ISender sender, ICurrentUserService currentUserService)
        : base(sender)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<WishListItemDetailsDto>> GetWishItemDetailInfo([FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        var userId = _currentUserService.GetCurrentUserId();
        return await HandleResultResponseForRequest(new GetWishListItemDetailQuery( userId, wishListName, itemId));
    }

    [HttpPost]
    public async Task<ActionResult> AddWishListItem([FromRoute] string wishListName, [FromBody] AddWishListItemRequest request)
    {
        return await HandleResultResponseForRequest(new AddItemToWishListCommand(wishListName, request.Name, request.Description, request.LinkUrl));
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveWishListItem([FromRoute] string wishListName, [FromBody] RemoveWishListItemRequest request)
    {
        return await HandleResultResponseForRequest(new RemoveWishListItemCommand(wishListName, request.ItemID));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWishListItem([FromRoute] string wishListName,
        [FromQuery] Guid itemId,
        [FromBody] UpdateWishListItemRequest request)
    {
        return await HandleResultResponseForRequest(new UpdateWishListItemCommand(wishListName, itemId, request.Name, request.Description, request.LinkUrl));
    }
}
