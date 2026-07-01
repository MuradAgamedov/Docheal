using Doccure.Web.UI.Dtos.MedicineDtos;
using Doccure.Web.UI.Services.MedicineServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicineController : Controller
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _medicineService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateMedicine()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine(CreateMedicineDto dto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(dto);

            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await _medicineService.UploadImageAsync(imageFile);
                if (imageUrl != null) dto.ImageUrl = imageUrl;
            }

            await _medicineService.CreateAsync(dto);
            TempData["Success"] = "Dərman uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteMedicine(int id)
        {
            await _medicineService.DeleteAsync(id);
            TempData["Success"] = "Dərman uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMedicine(int id)
        {
            var value = await _medicineService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Dərman tapılmadı!";
                return RedirectToAction("Index");
            }

            var dto = new UpdateMedicineDto
            {
                MedicineId = value.MedicineId,
                MedicineName = value.MedicineName,
                Barcode = value.Barcode,
                Stock = value.Stock,
                CriticalStockLevel = value.CriticalStockLevel,
                UnitPrice = value.UnitPrice,
                ExpirationDate = value.ExpirationDate,
                Status = value.Status
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMedicine(UpdateMedicineDto dto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(dto);

            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await _medicineService.UploadImageAsync(imageFile);
                if (imageUrl != null) dto.ImageUrl = imageUrl;
            }

            await _medicineService.UpdateAsync(dto);
            TempData["Success"] = "Dərman məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }
    }
}
