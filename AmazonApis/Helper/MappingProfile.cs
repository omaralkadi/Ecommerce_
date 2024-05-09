using AmazonApis.Dtos;
using AmazonCore.Entities;
using AmazonCore.Entities.Identity;
using AutoMapper;
using Talabat_Core.Entities;

namespace AmazonApis.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().
                ForMember(e => e.ProductType, o => o.MapFrom(a => a.ProductType.Name)).
                ForMember(e => e.ProductBrand, o => o.MapFrom(a => a.ProductBrand.Name)).
                ForMember(e => e.PictureUrl, o => o.MapFrom<PictureUrlResolver>()).
                ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }

    }
}
