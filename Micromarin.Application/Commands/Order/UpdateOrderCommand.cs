using MediatR;
using Micromarin.Application.DTOs.UpdateDtos;

namespace Micromarin.Application.Commands.Order;

public class UpdateOrderCommand : IRequest<bool>
{
    public UpdateOrderDto UpdateOrderDto { get; set; }
}
