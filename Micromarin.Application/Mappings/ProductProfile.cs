using AutoMapper;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.DTOs.UpdateDtos;
using Micromarin.Application.Entities;


namespace Micromarin.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand.Request, Product>().ReverseMap();
        CreateMap<UpdateProductDto, Product>().ReverseMap();
        CreateMap<GetProductDto, Product>().ReverseMap();
    }
}
