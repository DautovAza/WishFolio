using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.Services.UserProfile.Commands;
using WishFolio.Application.Services.UserProfile.Queries;

namespace WishFolio.WebApi.Controllers.UserProfile;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ISender _sender;

    public ProfileController(ISender sender) =>
        _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _sender.Send(new GetProfileQuery());
        return Ok(profile);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand request)
    {
        await _sender.Send(request);
        return Ok();
    }
}
