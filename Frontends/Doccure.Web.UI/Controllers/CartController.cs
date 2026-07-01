using Doccure.Web.UI.Dtos.CartDtos;
using Doccure.Web.UI.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        private string? UserId => HttpContext.Session.GetString("UserId");

        public async Task<IActionResult> Index()
        {
            if (UserId == null) return RedirectToAction("Login", "Auth");
            var items = await _cartService.GetCartAsync(UserId);
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string productName, decimal price, string? imageUrl, int quantity = 1)
        {
            if (UserId == null) return Json(new { success = false, message = "Daxil olun" });

            var item = new CartItemDto
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity,
                ImageUrl = imageUrl
            };
            await _cartService.AddToCartAsync(UserId, item);
            return Json(new { success = true, message = $"{productName} səbətə əlavə edildi!" });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            if (UserId != null)
                await _cartService.RemoveItemAsync(UserId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            if (UserId != null)
                await _cartService.ClearCartAsync(UserId);
            return RedirectToAction("Index");
        }
    }
}
