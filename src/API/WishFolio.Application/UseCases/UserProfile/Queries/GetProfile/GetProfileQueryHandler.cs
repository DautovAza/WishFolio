using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile;

public class GetProfileQueryHandler : RequestHandlerBase<GetProfileQuery, GetProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserProfileReadRepository _userProfileReadRepository;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(ICurrentUserService currentUserService,
        
        IUserProfileReadRepository userProfileReadRepository,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _userProfileReadRepository = userProfileReadRepository;
        _mapper = mapper;
    }

    public override async Task<Result<GetProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var userProfile = await _userProfileReadRepository.GetByIdAsync(userId); 

        if (userProfile is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        return Ok(_mapper.Map<GetProfileResponse>(userProfile));
    }
}
