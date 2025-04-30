using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class OrderService : IOrderService
{
    private readonly LaundryDbContext _context;

    public OrderService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await _context.Orders.Include(o => o.Customer)
        .Include(o => o.OrderItems)
        // .Select(o => new Order
        // {
        //     Id = o.Id,
        //     OrderDate = o.OrderDate,
        //     Status = o.Status,
        //     Customer = o.Customer
        // })
        .ToListAsync();

    public async Task<Order?> GetByIdAsync(long id) =>
        await _context.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Service)
        .Include(o => o.OrderImages)
        .AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

    public async Task<Order> CreateAsync(Order order)
    {
        // order.Customer = await _context.Customers.FindAsync(order.CustomerId);
        // _context.Orders.Add(order);
        // await _context.SaveChangesAsync();
        // return order;
        // Check if the customer exists
        var customer = await _context.Customers.FindAsync(order.CustomerId);
        if (customer == null)
        {
            // Return a BadRequest response if the customer does not exist
            throw new ArgumentException("Customer not found.");
        }

        // If the customer exists, assign it to the order
        order.Customer = customer;

        // Add the new order to the context and save changes
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> UpdateAsync(long id, Order order)
    {
        // if (id != order.Id) return false;
        // _context.Entry(order).State = EntityState.Modified;
        // await _context.SaveChangesAsync();
        // return true;
        var existingOrder = await _context.Orders
        .Include(o => o.OrderItems)
        .Include(o => o.OrderImages)
        .FirstOrDefaultAsync(o => o.Id == id);

        if (existingOrder == null) return false;

        // Update scalar properties
        existingOrder.Status = order.Status;
        existingOrder.TotalAmount = order.TotalAmount;
        existingOrder.OrderDate = order.OrderDate;
        existingOrder.CustomerId = order.CustomerId;
        existingOrder.OrderItems = order.OrderItems;
        // Optionally update OrderItems and OrderImages...
        // (either by clearing and adding new, or comparing/updating individually)

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return false;
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}
