using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class CustomerService : ICustomerService
{
    private readonly LaundryDbContext _context;

    public CustomerService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync() =>
        await _context.Customers.ToListAsync();

    public async Task<Customer?> GetByIdAsync(long id) =>
        await _context.Customers.FindAsync(id);

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateAsync(long id, Customer customer)
    {
        if (id != customer.Id) return false;
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
