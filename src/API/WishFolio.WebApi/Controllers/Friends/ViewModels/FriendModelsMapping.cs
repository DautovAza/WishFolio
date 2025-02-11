using AutoMapper;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;

namespace WishFolio.WebApi.Controllers.Friends.ViewModels;

public class FriendModelsMapping : Profile
{
    public FriendModelsMapping()
    {
        CreateMap<FriendDto, FriendModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));            
    }
}
