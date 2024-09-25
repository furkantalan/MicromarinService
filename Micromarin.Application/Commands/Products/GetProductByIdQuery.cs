


using MediatR;
using Micromarin.Application.DTOs.GetDtos;

namespace Micromarin.Application.Commands.Products;

public class GetProductByIdQuery : IRequest<GetProductDto>
{
    public Guid Id { get; set; }
}
