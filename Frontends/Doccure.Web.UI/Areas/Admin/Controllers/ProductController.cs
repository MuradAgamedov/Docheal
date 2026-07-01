using Doccure.Web.UI.Dtos.ProductDtos;
using Doccure.Web.UI.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct() => View();

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) return View(dto);

            if (imageFile != null && imageFile.Length > 0)
            {
                var url = await _productService.UploadImageAsync(imageFile);
                if (url != null) dto.ImageUrl = url;
            }

            await _productService.CreateAsync(dto);
            TempData["Success"] = "Məhsul uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var value = await _productService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Məhsul tapılmadı!";
                return RedirectToAction("Index");
            }

            var dto = new UpdateProductDto
            {
                ProductId = value.ProductId,
                ProductName = value.ProductName,
                Price = value.Price,
                Category = value.Category,
                ImageUrl = value.ImageUrl,
                Status = value.Status
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) return View(dto);

            if (imageFile != null && imageFile.Length > 0)
            {
                var url = await _productService.UploadImageAsync(imageFile);
                if (url != null) dto.ImageUrl = url;
            }

            await _productService.UpdateAsync(dto);
            TempData["Success"] = "Məhsul məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            TempData["Success"] = "Məhsul uğurla silindi!";
            return RedirectToAction("Index");
        }
    }
}
