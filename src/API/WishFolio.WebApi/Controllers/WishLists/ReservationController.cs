using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WishFolio.Application.UseCases.Wishlists;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/{userId:guid}/WishLists/{wishListName}/{itemId:guid}/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IWishListItemReservationService _wishListsService;

    public ReservationController(IWishListItemReservationService wishListsService) => 
        _wishListsService = wishListsService;

    [HttpPost]
    public async Task<IActionResult> ReserveItem([FromRoute] Guid itemId, [FromQuery] bool isAnonymous=false)
    {
        await _wishListsService.ReserveItem(itemId,isAnonymous);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> AcceptReservation([FromRoute] Guid itemId)
    {
        await _wishListsService.AcceptReservation(itemId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> CancelReservation([FromQuery] Guid itemId)
    {
        await _wishListsService.CancelReservation(itemId);
        return Ok();
    }
}
