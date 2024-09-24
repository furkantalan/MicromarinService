using MediatR;
using Micromarin.Application.DTOs.Products;
using Micromarin.Application.Enums;

namespace Micromarin.Application.Commands.Order;

public class CreateOrderCommand 
{
    public sealed record Request(
        string CustomerId,
        List<Guid> ProductIds,
        decimal TotalAmount,
        OrderStatus Status
        ) : IRequest<Response>;

    public sealed record Response(string CustomerId,
        List<Guid> ProductDtos,
        decimal TotalAmount,
        OrderStatus Status);
}
