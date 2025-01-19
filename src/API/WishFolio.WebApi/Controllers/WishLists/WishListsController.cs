using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WishFolio.Application.UseCases.Wishlists;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.WebApi.Controllers.WishLists.Dtos;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/{userId:guid}/[controller]")]
public class WishListsController : ControllerBase
{
    private readonly IWishListsService _wishListsService;
    private readonly ICurrentUserService _currentUserService;

    public WishListsController(IWishListsService wishListsService, ICurrentUserService currentUserService)
    {
        _wishListsService = wishListsService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishLists([FromRoute] Guid userId)
    {
        var wishLists = await _wishListsService.GetAllUserWishListsAsync(userId);
        return Ok(wishLists);
    }

    [HttpGet("{wishListName}/Details")]
    public async Task<IActionResult> GetWishItemDetailInfo([FromRoute] Guid userId, [FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        var wishLists = await _wishListsService.GetWishListItemDetailAsync(userId, wishListName, itemId);
        return Ok(wishLists);
    }

    [HttpPost]
    public async Task<IActionResult> AddWishList([FromBody] AddWishListRequest request)
    {
        await _wishListsService.AddWishListAsync(request.Name, request.Description, request.VisabilityLevel);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveWishList([FromBody] RemoveWishListRequest request)
    {
        await _wishListsService.RemoveWishListByNameAsync(request.Name);
        return Ok();
    }
}
