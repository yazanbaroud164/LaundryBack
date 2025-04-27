
namespace LaundryApi.Models;

public class Order
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string? Status { get; set; }
    public decimal TotalAmount { get; set; } 


    public Customer? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
