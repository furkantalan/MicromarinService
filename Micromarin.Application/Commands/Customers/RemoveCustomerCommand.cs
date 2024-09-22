using MediatR;


namespace Micromarin.Application.Commands.Customers;

/// <summary>
/// Handles the logic for removing a customer by Id.
/// </summary>
public class RemoveCustomerCommand : IRequest<bool>
{
    public Guid Id { get; }

    public RemoveCustomerCommand(Guid id)
    {
        Id = id;
    }
}
