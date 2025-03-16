using AutoMapper;
using WishFolio.Domain.Entities.ReadModels.Friends;

namespace WishFolio.Application.UseCases.Friends.Queries.Dtos;

public class FriendDtoMapping : Profile
{
    public FriendDtoMapping()
    {
        CreateMap<FriendReadModel, FriendDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
