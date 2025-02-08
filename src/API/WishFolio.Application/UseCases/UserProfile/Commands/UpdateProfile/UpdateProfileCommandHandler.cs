using WishFolio.Application.Common;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Repositories.Write;

namespace WishFolio.Application.UseCases.UserProfile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : RequestHandlerBase<UpdateProfileCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProfileCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        if (user is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var setNameResult = user.Profile.SetName(request.Name);
        var setAgeResult = user.Profile.SetAge(request.Age);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Combine([setNameResult, setAgeResult]);
    }
}
