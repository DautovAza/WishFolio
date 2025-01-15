using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WishFolio.Application.Services.Wishlists;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.WebApi.Controllers.WishLists.Dtos;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/WishLists/{wishListName}/[controller]")]
public class WishListsItemsManagmentController : ControllerBase
{
    private readonly IWishListsService _wishListsService;
    private readonly ICurrentUserService _currentUserService;

    public WishListsItemsManagmentController(IWishListsService wishListsService, ICurrentUserService currentUserService)
    {
        _wishListsService = wishListsService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishItemDetailInfo([FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var wishLists = await _wishListsService.GetWishListItemDetailAsync(userId, wishListName, itemId);
        return Ok(wishLists);
    }

    [HttpPost]
    public async Task<IActionResult> AddWishListItem([FromRoute] string wishListName, [FromBody] AddWishListItemRequest request)
    {
        await _wishListsService.AddWishListItemAsync(wishListName, request.Name, request.Description, request.LinkUrl);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveWishListItem([FromRoute] string wishListName, [FromBody] RemoveWishListItemRequest request)
    {
        await _wishListsService.RemoveWishListItemAsync(wishListName, request.ItemID);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWishListItem([FromRoute] string wishListName,
        [FromQuery] Guid itemId,
        [FromBody] UpdateWishListItemRequest request)
    {
        await _wishListsService.UpdateWishListItemAsync(wishListName, itemId, request.Name, request.Description, request.LinkUrl);
        return Ok();
    }
}
