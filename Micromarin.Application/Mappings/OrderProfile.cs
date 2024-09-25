using AutoMapper;
using Micromarin.Application.DTOs.Products;
using Micromarin.Application.DTOs.UpdateDtos;
using Micromarin.Application.Entities;


namespace Micromarin.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // UpdateOrderDto ile Order arasındaki eşleştirme
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore()); // Products manuel olarak güncellenecek

        // ProductDto ile Product arasındaki eşleştirme
        CreateMap<ProductDto, Product>();

    }
}
