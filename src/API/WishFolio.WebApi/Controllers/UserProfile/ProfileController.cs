using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.UseCases.UserProfile.Queries.Dtos;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile;
using WishFolio.Application.UseCases.UserProfile.Commands.UpdateProfile;
using WishFolio.WebApi.Controllers.Abstractions;

namespace WishFolio.WebApi.Controllers.UserProfile;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ResultHandlerControllerBase
{
    public ProfileController(ISender sender) :
        base(sender)
    { }

    [HttpGet]
    public Task<ActionResult<GetProfileResponse>> GetProfile()
    {
        return HandleResultResponseForRequest(new GetProfileQuery());
    }

    [HttpPut]
    public Task<ActionResult> UpdateProfile([FromBody] UpdateProfileCommand request)
    {
        return HandleResultResponseForRequest(request);
    }
}