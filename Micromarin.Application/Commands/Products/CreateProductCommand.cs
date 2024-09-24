
using MediatR;
using Micromarin.Application.DTOs.CreateDtos;

namespace Micromarin.Application.Commands.Products;

public class CreateProductCommand : IRequest<bool>
{
    public CreateProductDto CreateProductDto { get; set; }

}
