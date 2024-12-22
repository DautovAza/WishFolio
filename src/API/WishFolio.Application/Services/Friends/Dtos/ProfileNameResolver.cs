using AutoMapper;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.Services.Friends.Dtos;

public class ProfileNameResolver : IValueResolver<Friendship, FriendDto, string>
{
    private readonly IUserRepository _userRepository;

    public ProfileNameResolver(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string Resolve(Friendship source, FriendDto destination, string destMember, ResolutionContext context)
    {
        return _userRepository.GetProfileByIdAsync(source.FriendId).Result.Name;
    }
}
