using AutoMapper;
using WishFolio.Domain.Abstractions.ReadModels.Users;

namespace WishFolio.Application.UseCases.UserProfile.Queries.Dtos;

public class GetProfileResponseMapping : Profile
{
    public GetProfileResponseMapping()
    {
        CreateMap<UserProfileReadModel, GetProfileResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }

}