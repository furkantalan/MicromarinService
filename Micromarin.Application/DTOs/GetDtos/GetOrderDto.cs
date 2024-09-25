using Micromarin.Application.DTOs.Products;
using Micromarin.Application.Enums;

namespace Micromarin.Application.DTOs.GetDtos;

public class GetOrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<ProductDto>? Products { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
