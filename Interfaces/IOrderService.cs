using System;
using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(long id, Order order);
    Task<bool> DeleteAsync(long id);
}
