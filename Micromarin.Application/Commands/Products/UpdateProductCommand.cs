
using MediatR;
using Micromarin.Application.DTOs.UpdateDtos;

namespace Micromarin.Application.Commands.Products;

public class UpdateProductCommand : IRequest<bool>
{
    public UpdateProductDto UpdateProductDto { get; set; }

}
