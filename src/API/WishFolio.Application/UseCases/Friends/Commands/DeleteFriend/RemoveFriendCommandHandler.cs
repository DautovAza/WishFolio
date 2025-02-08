using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Friends.Commands.DeleteFriend;

public class RemoveFriendCommandHandler : RequestHandlerBase<RemoveFriendCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFriendCommandHandler(ICurrentUserService currentUserService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var user = await _userRepository.GetByIdAsync(userId);
        var friend = await _userRepository.GetByIdAsync(request.FriendId);

        if (user == null || friend == null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var result = user.RemoveFriend(friend);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
