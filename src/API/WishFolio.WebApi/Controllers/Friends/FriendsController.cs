﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AutoMapper;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.DeleteFriend;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.Friends.ViewModels;
using WishFolio.WebApi.Controllers.Common.Models;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController : MappingResultHandlerControllerBase
{

    public FriendsController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    { }

    [HttpGet]
    public Task<ActionResult<PagedCollection<FriendModel>>> GetFriends(RequestFilteringModel filteringModel, RequestPageModel pageModel)
    {
        var filteringInfo = new FilteringInfo
        {
            FilterName = filteringModel.FilterName,
            OrderBy = filteringModel.OrderBy
        };
        var pageInfo = new PageInfo
        {
            CurrentPageNumber = pageModel.PageNumber,
            PageSize = pageModel.PageSize,
        };
        return HandleRequestResult<PagedCollection<FriendDto>, PagedCollection<FriendModel>>(new GetFriendsQuery(FriendshipStatus.Accepted,filteringInfo, pageInfo));
    }

    [HttpDelete("{friendId}")]
    public Task<ActionResult> RemoveFriend(Guid friendId)
    {
        return HandleRequestResult(new RemoveFriendCommand() { FriendId = friendId });
    }
}
