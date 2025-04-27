using System;

namespace LaundryApi.Models;

public class Customer
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
