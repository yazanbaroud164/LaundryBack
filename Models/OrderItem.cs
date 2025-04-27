using System;

namespace LaundryApi.Models;

public class OrderItem
{
    public long Id { get; set; }
    public long? OrderId { get; set; }
    public long ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Order? Order { get; set; }
    public Service? Service { get; set; }
}
