using AutoMapper;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.AddWishListItem;
using WishFolio.Application.UseCases.Wishlists.Commands.Items.UpdateWishListItem;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;

namespace WishFolio.WebApi.Controllers.WishLists.ViewModels.Items;

public class WishListItemModelsMapping : Profile
{
    public WishListItemModelsMapping()
    {
        CreateMap<AddWishListItemModel, AddItemToWishListCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LinkUrl));

        CreateMap<UpdateWishListItemModel, UpdateWishListItemCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LinkUrl));

        CreateMap<WishListItemDto, WishListItemModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<WishListItemDetailsDto, WishListItemDetailedModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri))
            .ForMember(dest => dest.ReservedBy, opt => opt.MapFrom(src => src.ReservedBy))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
