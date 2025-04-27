using System;
using LaundryApi.Data;
using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryApi.Services;

public class ProductService : IProductService
{
    private readonly LaundryDbContext _context;

    public ProductService(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(long id) =>
        await _context.Products.FindAsync(id);

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateAsync(long id, Product product)
    {
        if (id != product.Id) return false;
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
