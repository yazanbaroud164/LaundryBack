using System;
using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(long id);
    Task<OrderItem> CreateAsync(OrderItem orderItem);
    Task<bool> UpdateAsync(long id, OrderItem orderItem);
    Task<bool> DeleteAsync(long id);
}
