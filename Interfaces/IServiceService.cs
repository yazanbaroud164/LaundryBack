using System;
using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(long id);
    Task<Service> CreateAsync(Service service);
    Task<bool> UpdateAsync(long id, Service service);
    Task<bool> DeleteAsync(long id);
}
