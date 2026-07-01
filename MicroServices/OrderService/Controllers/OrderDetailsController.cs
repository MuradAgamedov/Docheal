using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos.OrderDetailDtos;
using OrderService.Services.OrderDetailServices;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetAllByOrderId(int orderId)
        {
            var values = await _orderDetailService.GetAllByOrderIdAsync(orderId);
            return Ok(values);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var value = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (value == null)
                return NotFound("Order detail not found");

            return Ok(value);
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto, int orderId)
        {
            await _orderDetailService.CreateOrderDetailAsync(createOrderDetailDto, orderId);
            return Ok("Order detail created successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id);
            return Ok("Order detail deleted successfully");
        }
    }
}
