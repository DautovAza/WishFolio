using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.GetProfileById;

public class GetProfileByIdQueryHandler : RequestHandlerBase<GetProfileByIdQuery, GetProfileDto>
{
    private readonly IUserProfileReadRepository _userProfileReadRepository;
    private readonly IMapper _mapper;

    public GetProfileByIdQueryHandler(IUserProfileReadRepository userProfileReadRepository,
        IMapper mapper)
    {
        _userProfileReadRepository = userProfileReadRepository;
        _mapper = mapper;
    }

    public override async Task<Result<GetProfileDto>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileReadRepository.GetByIdAsync(request.Id);

        if (userProfile is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        return Ok(_mapper.Map<GetProfileDto>(userProfile));
    }
}
