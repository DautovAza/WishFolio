using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AutoMapper;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.Common.Models;
using WishFolio.WebApi.Controllers.Friends.ViewModels;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.Application.UseCases.Friends.Commands.CreateFriendRequest;
using WishFolio.Application.UseCases.Friends.Commands.AcceptFriendRequest;
using WishFolio.Application.UseCases.Friends.Commands.RejectFriendRequest;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/friends/[controller]")]
public class RequestsController : MappingResultHandlerControllerBase
{
    public RequestsController(ISender sender,IMapper mapper) 
        : base(sender, mapper)
    {
    }

    [HttpPost()]
    public  Task<ActionResult> AddFriend([FromBody] AddFriendModel request)
    {
        if (request.UserId.HasValue)
        {
            return  HandleRequestResult(new CreateFriendRequestByIdCommand(request.UserId.Value));
        }
        else
        {
            return  HandleRequestResult(new CreateFriendRequestByEmailCommand(request.UserEmail));
        }
    }

    [HttpPut("{friendId}/accept")]
    public  Task<ActionResult> AcceptRequest(Guid friendId)
    {
        return  HandleRequestResult(new AcceptFriendRequestCommand(friendId));
    }

    [HttpPut("{friendId}/reject")]
    public  Task<ActionResult> RejectRequest(Guid friendId)
    {
        return  HandleRequestResult(new RejectFriendRequestCommand(friendId));
    }

    [HttpGet("incoming")]
    public Task<ActionResult<PagedCollection<FriendModel>>> GetIncomingRequests(RequestFilteringModel filteringModel, RequestPageModel pageModel)
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
        return  HandleRequestResult<PagedCollection<FriendDto>, PagedCollection<FriendModel>>(new GetFriendsQuery(FriendshipStatus.Requested, filteringInfo, pageInfo));
    }

    [HttpGet("sent")]
    public  Task<ActionResult<PagedCollection<FriendModel>>> GetSentRequests(RequestFilteringModel filteringModel, RequestPageModel pageModel)
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
        return HandleRequestResult<PagedCollection<FriendDto>, PagedCollection<FriendModel>>(new GetFriendsQuery(FriendshipStatus.Pending, filteringInfo, pageInfo));
    }
}
