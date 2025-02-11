using AutoMapper;
using WishFolio.Application.UseCases.UserProfile.Commands.UpdateProfile;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
using WishFolio.WebApi.Controllers.UserProfile.Models;

namespace WishFolio.WebApi.Controllers.UserProfile.ViewModels;

public class UserProfileModelsMapping : Profile
{
    public UserProfileModelsMapping()
    {
        CreateMap<GetDetailedProfileDto, DetailedUserProfileModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<GetProfileDto, UserProfileModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));   
        
        CreateMap<UpdateUseProfileModel, UpdateProfileCommand>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age));
    }

}
