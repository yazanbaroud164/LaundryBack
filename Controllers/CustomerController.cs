using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll() =>
            Ok(await _customerService.GetAllAsync());

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Customer>> GetById(long id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            var newCustomer = await _customerService.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Update(long id, Customer customer)
        {
            var success = await _customerService.UpdateAsync(id, customer);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _customerService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
