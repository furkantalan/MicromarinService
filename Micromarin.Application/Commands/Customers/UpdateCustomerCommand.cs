
using MediatR;
using Micromarin.Application.DTOs.UpdateDtos;

namespace Micromarin.Application.Commands.Customers;

public class UpdateCustomerCommand : IRequest<bool>
{
    public UpdateCustomerDto UpdateCustomerDto { get; set; }
}
