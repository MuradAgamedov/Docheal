using Doccure.Web.UI.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService) => _productService = productService;

        public async Task<IActionResult> Index(string? category, string? search)
        {
            var products = await _productService.GetAllAsync();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category).ToList();

            if (!string.IsNullOrEmpty(search))
                products = products.Where(p => p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.Categories = products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
            ViewBag.CurrentCategory = category;
            ViewBag.Search = search;

            return View(products);
        }

        public IActionResult Cart() => RedirectToAction("Index", "Cart");
    }
}
