using AutoMapper;
using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Application.Services.UserProfile.Queries.Dtos;

public class GetProfileResponseMapping : Profile
{
    public GetProfileResponseMapping()
    {
        CreateMap<User, GetProfileResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Profile.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Profile.Age))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address));
    }

}