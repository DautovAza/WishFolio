using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile;

public class GetProfileQueryHandler : RequestHandlerBase<GetProfileQuery, GetProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(ICurrentUserService currentUserService, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<Result<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        if (user is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        return Ok(_mapper.Map<GetProfileResponse>(user));
    }
}
