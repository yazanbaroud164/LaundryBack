using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll() =>
            Ok(await _orderService.GetAllAsync());

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<Order>> GetById(long id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            var newOrder = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> Update(long id, Order order)
        {
            var success = await _orderService.UpdateAsync(id, order);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _orderService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
