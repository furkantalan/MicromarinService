
using MediatR;

namespace Micromarin.Application.Commands.Products;

public class CreateProductCommand
{
    public sealed record Request(
        string Name,
        string Description,
        decimal Price,
        int StockQuantity
        ) : IRequest<Response>;
    public sealed record Response(bool result);

}
