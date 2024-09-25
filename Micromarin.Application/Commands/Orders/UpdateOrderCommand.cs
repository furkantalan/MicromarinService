using MediatR;
using Micromarin.Application.DTOs.UpdateDtos;


namespace Micromarin.Application.Commands.Orders;

public class UpdateOrderCommand : IRequest<bool>
{
    public UpdateOrderDto UpdateOrderDto { get; set; }

}