using System;
using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<Product> CreateAsync(Product product);
    Task<bool> UpdateAsync(long id, Product product);
    Task<bool> DeleteAsync(long id);
}
