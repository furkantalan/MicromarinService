﻿

using Micromarin.Domain.Entities;

namespace Micromarin.Application.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
