

using MediatR;
using Micromarin.Application.DTOs.GetDtos;

namespace Micromarin.Application.Commands.Customers;

public class GetCustomerByIdQuery : IRequest<GetCustomerDto>
{
    public Guid Id { get; set; }
}
