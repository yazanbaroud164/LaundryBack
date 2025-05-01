using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class OrderItemService : IOrderItemService
{
    private readonly LaundryDbContext _context;

    public OrderItemService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync() =>
        await _context.OrderItems.AsNoTracking().ToListAsync();

    public async Task<OrderItem?> GetByIdAsync(long id) =>
        await _context.OrderItems.FindAsync(id);

    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return orderItem;
    }

    public async Task<bool> UpdateAsync(long id, OrderItem orderItem)
    {
        if (id != orderItem.Id) return false;
        _context.Entry(orderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null) return false;
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
