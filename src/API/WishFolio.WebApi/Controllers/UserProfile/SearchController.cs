using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.Application.UseCases.UserProfile.Queries.SearchUsers;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.UserProfile.Models;

namespace WishFolio.WebApi.Controllers.UserProfile;

[Route("api/users/[controller]")]
public class SearchController : MappingResultHandlerControllerBase
{
    public SearchController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
    }

    [HttpGet]
    public Task<ActionResult<PagedCollection<UserProfileModel>>> SearchUsers(string? searchName, string? orderBy, int pageNumber = 1, int pageSize = 5)
    {
        return HandleRequestResult<PagedCollection<GetProfileDto>, PagedCollection<UserProfileModel>>(new SearchUsersQuery(searchName, orderBy, pageNumber, pageSize));
    }
}
