using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class ServiceService : IServiceService
{
    private readonly LaundryDbContext _context;

    public ServiceService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetAllAsync() =>
        await _context.Services.ToListAsync();

    public async Task<Service?> GetByIdAsync(long id) =>
        await _context.Services.FindAsync(id);

    public async Task<Service> CreateAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<bool> UpdateAsync(long id, Service service)
    {
        if (id != service.Id) return false;
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return false;
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return true;
    }
}
