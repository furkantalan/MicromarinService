using MediatR;
using Micromarin.Application.DTOs.CreateDtos;

namespace Micromarin.Application.Commands.Orders;

public class CreateOrderCommand : IRequest<bool>
{
    public CreateOrderDto CreateOrderDto { get; set; }

}
