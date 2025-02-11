using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.ReserveItem;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.AcceptReservation;
using WishFolio.Application.UseCases.WishListItemsReservations.Commands.CancelReservation;

namespace WishFolio.WebApi.Controllers.WishLists;

[Authorize]
[ApiController]
[Route("api/{userId:guid}/WishLists/{wishListName}/{itemId:guid}/[controller]")]
public class ReservationController : ResultHandlerControllerBase
{
    public ReservationController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> ReserveItem([FromRoute] Guid itemId, [FromQuery] bool isAnonymous = false)
    {
        return await HandleRequestResult(new ReserveItemCommand(itemId, isAnonymous));
    }

    [HttpPut]
    public async Task<IActionResult> AcceptReservation([FromRoute] Guid itemId)
    {
        return await HandleRequestResult(new AcceptReservationItemCommand(itemId));
    }

    [HttpDelete]
    public async Task<IActionResult> CancelReservation([FromQuery] Guid itemId)
    {
        return await HandleRequestResult(new CancelReservationItemCommand(itemId));
    }
}
