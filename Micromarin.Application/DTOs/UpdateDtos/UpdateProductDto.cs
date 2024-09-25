﻿

namespace Micromarin.Application.DTOs.UpdateDtos;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
