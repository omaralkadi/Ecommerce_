using AmazonApis.Dtos;
using AmazonCore.Entities;
using AmazonCore.Entities.Identity;
using AmazonCore.Entities.Order;
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

            CreateMap<AmazonCore.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<AmazonCore.Entities.Order.Address, AddressDto>().ReverseMap();
            CreateMap<OrderDto, Order>();

            CreateMap<Order, OrderToReturnDto>().
                ForMember(o => o.deliveryMethod, p => p.MapFrom(o => o.deliveryMethod.ShortName)).
                ForMember(o => o.deliveryMethodCost, p => p.MapFrom(o => o.deliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>().
                ForMember(e => e.ProductId, o => o.MapFrom(o => o.Product.ProductId)).
                ForMember(e => e.ProductName, o => o.MapFrom(o => o.Product.ProductName)).
                ForMember(e => e.PictureUrl, o => o.MapFrom(o => o.Product.PictureUrl)).
                ForMember(e => e.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());


        }

    }
}
