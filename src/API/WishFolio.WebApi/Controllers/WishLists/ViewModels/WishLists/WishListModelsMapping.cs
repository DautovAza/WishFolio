using AutoMapper;
using WishFolio.Domain.Entities.WishListAgregate;
using WishFolio.Application.UseCases.Wishlists.Commands.CreateWishList;
using WishFolio.Application.UseCases.Wishlists.Commands.RemoveWishList;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.WebApi.Controllers.WishLists.ViewModels.WishLists;

public class WishListModelsMapping : Profile
{
    public WishListModelsMapping()
    {
        CreateMap<AddWishListModel, CreateWishListCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.VisabilityLevel, opt => opt.MapFrom(src => ParceVisabilityLevel(src.VisabilityLevel)));

        CreateMap<RemoveWishListModel, RemoveWishListByNameCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<WishListDto, WishListModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.VisabilityLevel, opt => opt.MapFrom(src => src.VisabilityLevel.ToString()))
            .ForMember(dest => dest.Items , opt => opt.MapFrom(src =>src.Items);
    }

    private static VisabilityLevel ParceVisabilityLevel(string visabilityLevelString)
    {
        return Enum.Parse<VisabilityLevel>(visabilityLevelString);
    }
}
