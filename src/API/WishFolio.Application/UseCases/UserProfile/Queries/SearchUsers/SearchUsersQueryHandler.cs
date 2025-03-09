using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Application.UseCases.UserProfile.Queries.SearchUsers;

public class SearchUsersQueryHandler : RequestHandlerBase<SearchUsersQuery, PagedCollection<GetProfileDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserProfileReadRepository _userProfileReadRepository;

    public SearchUsersQueryHandler(IMapper mapper, IUserProfileReadRepository userProfileReadRepository)
    {
        _mapper = mapper;
        _userProfileReadRepository = userProfileReadRepository;
    }

    public override async Task<Result<PagedCollection<GetProfileDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var pagedUsers = await _userProfileReadRepository.GetFiltredUsersAsync(request.FilteringString, request.OrderBy, request.PageSize, request.PageNumber);
        return Ok(_mapper.Map<PagedCollection<GetProfileDto>>(pagedUsers));

    }
}
