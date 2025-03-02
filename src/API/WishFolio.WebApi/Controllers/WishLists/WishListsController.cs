using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Application.UseCases.Wishlists.Queries.GetWishLists;
using WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;
using WishFolio.Application.UseCases.Wishlists.Commands.CreateWishList;
using WishFolio.Application.UseCases.Wishlists.Commands.RemoveWishList;
using AutoMapper;
using WishFolio.WebApi.Controllers.WishLists.ViewModels.WishLists;
using WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;
using WishFolio.Application.UseCases.Wishlists.Queries.GetWishlistItems;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/{userId:guid}/[controller]")]
public class WishListsController : MappingResultHandlerControllerBase
{
    public WishListsController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
    }

    [HttpGet]
    public Task<ActionResult<IEnumerable<WishListModel>>> GetWishLists([FromRoute] Guid userId)
    {
        return HandleRequestResult<IEnumerable<WishListDto>, IEnumerable<WishListModel>>(new GetUserWishListsQuery(userId));
    }

    [HttpGet("{wishListName}/Details")]
    public async Task<ActionResult<WishListItemDetailedModel>> GetWishItemDetailInfo([FromRoute] Guid userId, [FromRoute] string wishListName, [FromQuery] Guid itemId)
    {
        return await HandleRequestResult<WishListItemDetailsDto, WishListItemDetailedModel>(new GetWishListItemDetailQuery(userId, wishListName, itemId));
    }   
    
    [HttpGet("{wishListName}")]
    public async Task<ActionResult<PagedCollection<WishListItemModel>>> GetWishlistItems([FromRoute] Guid userId, [FromRoute] string wishListName, [FromQuery] int pageNumber=1,int pageSize=3)
    {
        return await HandleRequestResult<PagedCollection<WishListItemDto>, PagedCollection< WishListItemModel>>(new GetWishlistItemsQuery(userId, wishListName,pageNumber,pageSize));
    }

    [HttpPost]
    public async Task<ActionResult> AddWishList([FromBody] AddWishListModel request)
    {
        var command = Mapper.Map<CreateWishListCommand>(request);
        return await HandleRequestResult(command);
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveWishList([FromBody] RemoveWishListModel request)
    {
        var command = Mapper.Map<RemoveWishListByNameCommand>(request);
        return await HandleRequestResult(command);
    }
}
