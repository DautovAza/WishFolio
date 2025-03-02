using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AutoMapper;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.AddWishListItem;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.UpdateWishListItem;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.RemoveWishListItem;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/WishLists/{wishListName}/[controller]")]
public class WishListItemsManagmentController : MappingResultHandlerControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public WishListItemsManagmentController(ISender sender, ICurrentUserService currentUserService, IMapper mapper)
        : base(sender, mapper)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public Task<ActionResult<WishListItemDetailedModel>> GetWishItemDetailInfo([FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        var userId = _currentUserService.GetCurrentUserId();
        return HandleRequestResult<WishListItemDetailsDto, WishListItemDetailedModel>(new GetWishListItemDetailQuery(userId, wishListName, itemId));
    }

    [HttpPost]
    public Task<ActionResult> AddWishListItem([FromRoute] string wishListName, [FromBody] AddWishListItemModel request)
    {
        var command = Mapper.Map<AddItemToWishListCommand>(request);
        command.WishListName = wishListName;
        return HandleRequestResult(command);
    }

    [HttpDelete]
    public Task<ActionResult> RemoveWishListItem([FromRoute] string wishListName, [FromBody] RemoveWishListItemModel request)
    {
        return HandleRequestResult(new RemoveWishListItemCommand(wishListName, request.ItemID));
    }

    [HttpPut]
    public Task<ActionResult> UpdateWishListItem([FromRoute] string wishListName,
        [FromQuery] Guid itemId,
        [FromBody] UpdateWishListItemModel request)
    {
        var command = Mapper.Map<UpdateWishListItemCommand>(request);
        command.WishListName = wishListName;
        command.Id = itemId;
        return HandleRequestResult(command);
    }
}
