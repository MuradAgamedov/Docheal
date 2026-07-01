using Doccure.MarketService.Dtos.CartDtos;
using Doccure.MarketService.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.MarketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var items = await _cartService.GetCartAsync(userId);
            return Ok(items);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var item = new CartItemDto
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                ImageUrl = request.ImageUrl
            };
            await _cartService.AddItemToCartAsync(request.UserId, item);
            return Ok(new { message = "Məhsul səbətə əlavə edildi" });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveItem([FromQuery] string userId, [FromQuery] int productId)
        {
            await _cartService.RemoveItemAsync(userId, productId);
            return Ok(new { message = "Məhsul səbətdən silindi" });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok(new { message = "Səbət təmizləndi" });
        }
    }
}
