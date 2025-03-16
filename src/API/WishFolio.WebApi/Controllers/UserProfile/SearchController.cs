using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.Application.UseCases.UserProfile.Queries.SearchUsers;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.Common.Models;
using WishFolio.WebApi.Controllers.UserProfile.Models;

namespace WishFolio.WebApi.Controllers.UserProfile;

[Authorize]
[Route("api/users/[controller]")]
public class SearchController : MappingResultHandlerControllerBase
{
    public SearchController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
    }

    [HttpGet]
    public Task<ActionResult<PagedCollection<UserProfileModel>>> SearchUsers(RequestFilteringModel filteringModel, RequestPageModel requestPage)
    {
        return HandleRequestResult<PagedCollection<GetProfileDto>, PagedCollection<UserProfileModel>>(
            new SearchUsersQuery(filteringModel.FilterName, filteringModel.OrderBy, requestPage.PageNumber, requestPage.PageSize));
    }
}