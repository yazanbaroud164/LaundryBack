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
        private readonly IOrderImageService _orderImageService;


        public OrderController(IOrderService orderService, IOrderImageService orderImageService)
        {
            _orderService = orderService;
            _orderImageService = orderImageService;
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



        public class UpdateStatusDto
        {
            public string Status { get; set; } = string.Empty;
        }
        [HttpPut("UpdateStatusOrder/{id}")]
        public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateStatusDto dto)
        {
            var success = await _orderService.UpdateStatusAsync(id, dto.Status);
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

        [HttpPost("{orderId}/images")]
        public async Task<IActionResult> UploadImages(long orderId, [FromForm] List<IFormFile> files)
        {
            try
            {
                await _orderImageService.UploadImagesAsync(orderId, files);
                return Ok(new { Message = "Images uploaded successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Order not found.");
            }
        }

        [HttpGet("{orderId}/images")]
        public async Task<IActionResult> GetImages(long orderId)
        {
            var images = await _orderImageService.GetImagesByOrderIdAsync(orderId);
            var result = images.Select(img => new
            {
                img.Id,
                img.FileName,
                img.ContentType,
                Base64 = Convert.ToBase64String(img.Data)
            });

            return Ok(result);
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImage(long imageId)
        {
            await _orderImageService.DeleteImageAsync(imageId);
            return NoContent();
        }


    }
}
