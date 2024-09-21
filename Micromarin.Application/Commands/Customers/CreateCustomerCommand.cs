using MediatR;

namespace Micromarin.Application.Commands.Customers;

public class CreateCustomerCommand
{
    public sealed record Request(
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Address
        ) : IRequest<Response>;
    public sealed record Response(bool result);
}
