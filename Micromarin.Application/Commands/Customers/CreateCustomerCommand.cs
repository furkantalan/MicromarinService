using MediatR;
using Micromarin.Application.DTOs.CreateDtos;

namespace Micromarin.Application.Commands.Customers;

public class CreateCustomerCommand : IRequest<bool>
{
    public CreateCustomerDto CreateCustomerDto { get; set; }
}
