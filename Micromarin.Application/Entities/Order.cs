

using Micromarin.Application.Enums;
using Micromarin.Domain.Entities;

namespace Micromarin.Application.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public List<Product>? Products { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
