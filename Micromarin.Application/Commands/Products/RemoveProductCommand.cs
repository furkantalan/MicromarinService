
using MediatR;

namespace Micromarin.Application.Commands.Products;

public class RemoveProductCommand : IRequest<bool>
{
    public Guid Id { get; }

    public RemoveProductCommand(Guid id)
    {
        Id = id;
    }
}
