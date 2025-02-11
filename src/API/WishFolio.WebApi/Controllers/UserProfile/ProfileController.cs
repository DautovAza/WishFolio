using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.UseCases.UserProfile.Commands.UpdateProfile;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.GetOwnerProfile;
using WishFolio.WebApi.Controllers.UserProfile.Models;

namespace WishFolio.WebApi.Controllers.UserProfile;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : MappingResultHandlerControllerBase
{
    public ProfileController(ISender sender, IMapper mapper) :
        base(sender, mapper)
    { }

    [HttpGet]
    public Task<ActionResult<DetailedUserProfileModel>> GetProfile()
    {
        return HandleRequestResult<GetDetailedProfileDto, DetailedUserProfileModel>(new GetOwnerProfileQuery());
    }   
    
    [HttpPut]
    public Task<ActionResult> UpdateProfile([FromBody] UpdateProfileCommand request)
    {
        return HandleRequestResult(request);
    }
}