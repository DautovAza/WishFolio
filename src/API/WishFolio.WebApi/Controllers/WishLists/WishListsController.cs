using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.WishLists.Dtos;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Application.UseCases.Wishlists.Queries.GetWishLists;
using WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;
using WishFolio.Application.UseCases.Wishlists.Commands.CreateWishList;
using WishFolio.Application.UseCases.Wishlists.Commands.RemoveWishList;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/{userId:guid}/[controller]")]
public class WishListsController : ResultHandlerControllerBase
{
    public WishListsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishListDto>>> GetWishLists([FromRoute] Guid userId)
    {
        return await HandleResultResponseForRequest(new GetUserWishListsQuery(userId));
    }

    [HttpGet("{wishListName}/Details")]
    public async Task<ActionResult<WishListItemDetailsDto>> GetWishItemDetailInfo([FromRoute] Guid userId, [FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        return await HandleResultResponseForRequest(new GetWishListItemDetailQuery(userId, wishListName, itemId));
    }

    [HttpPost]
    public async Task<ActionResult> AddWishList([FromBody] AddWishListRequest request)
    {
        return await HandleResultResponseForRequest(new CreateWishListCommand(request.Name, request.Description, request.VisabilityLevel));
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveWishList([FromBody] RemoveWishListRequest request)
    {
        return await HandleResultResponseForRequest(new RemoveWishListByNameCommand(request.Name));
    }
}
