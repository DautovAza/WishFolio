using AutoMapper;
using MediatR;
using WishFolio.Application.Services.UserProfile.Queries;
using WishFolio.Application.Services.UserProfile.Queries.Dtos;
using WishFolio.Domain.Abstractions.Auth;

namespace WishFolio.Application.Services.UserProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(ICurrentUserService currentUserService, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<GetProfileResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        return _mapper.Map<GetProfileResponse>(user);
    }
}
