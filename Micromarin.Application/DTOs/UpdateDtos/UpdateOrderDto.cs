using Micromarin.Application.DTOs.Products;
using Micromarin.Application.Enums;

namespace Micromarin.Application.DTOs.UpdateDtos;


public class UpdateOrderDto
{
    public Guid Id { get; set; }
    public List<ProductDto>? Products { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}