using MediatR;
using Micromarin.Application.DTOs.GetDtos;

namespace Micromarin.Application.Commands.Orders;

public class GetOrderByIdQuery : IRequest<GetOrderDto>
{
    public Guid Id { get; set; }
}
