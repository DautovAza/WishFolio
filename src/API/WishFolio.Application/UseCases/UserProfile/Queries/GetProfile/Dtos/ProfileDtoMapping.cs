using AutoMapper;
using WishFolio.Domain.Abstractions.ReadModels.Users;

namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;

public class ProfileDtoMapping : Profile
{
    public ProfileDtoMapping()
    {
        CreateMap<UserProfileReadModel, GetDetailedProfileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<UserProfileReadModel, GetProfileDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}