using System;

namespace LaundryApi.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
