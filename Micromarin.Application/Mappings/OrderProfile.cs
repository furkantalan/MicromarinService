using AutoMapper;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.DTOs.Products;
using Micromarin.Application.DTOs.UpdateDtos;
using Micromarin.Application.Entities;


namespace Micromarin.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<ProductDto, Product>();

        CreateMap<Order, GetOrderDto>();
        CreateMap<GetOrderDto, Order>();


    }
}
