using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("GetAllServices")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll() =>
            Ok(await _serviceService.GetAllAsync());

        [HttpGet("GetServiceById/{id}")]
        public async Task<ActionResult<Service>> GetById(long id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpPost("CreateService")]
        public async Task<ActionResult<Service>> Create(Service service)
        {
            var newService = await _serviceService.CreateAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = newService.Id }, newService);
        }

        [HttpPut("UpdateService/{id}")]
        public async Task<IActionResult> Update(long id, Service service)
        {
            var success = await _serviceService.UpdateAsync(id, service);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("DeleteService/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _serviceService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
