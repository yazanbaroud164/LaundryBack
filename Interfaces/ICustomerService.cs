using LaundryApi.Models;

namespace LaundryApi.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(long id);
    Task<Customer> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(long id, Customer customer);
    Task<bool> DeleteAsync(long id);
}
