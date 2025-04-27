using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll() =>
            Ok(await _orderItemService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById(long id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem orderItem)
        {
            var newOrderItem = await _orderItemService.CreateAsync(orderItem);
            return CreatedAtAction(nameof(GetById), new { id = newOrderItem.Id }, newOrderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, OrderItem orderItem)
        {
            var success = await _orderItemService.UpdateAsync(id, orderItem);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _orderItemService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
