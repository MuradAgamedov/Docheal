using Doccure.Web.UI.Dtos.BranchDtos;
using Doccure.Web.UI.Services.BranchServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _branchService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createBranchDto);
            }
            await _branchService.CreateAsync(createBranchDto);
            TempData["Success"] = "Şöbə uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBranch(string id)
        {
            await _branchService.DeleteAsync(id);
            TempData["Success"] = "Şöbə uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBranch(string id)
        {
            var value = await _branchService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Şöbə tapılmadı!";
                return RedirectToAction("Index");
            }
            var updateBranchDto = new UpdateBranchDto
            {
                BranchId = value.BranchId,
                BranchName = value.BranchName,
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                Status = value.Status,
                BranchCode = value.BranchCode,
                DoctorCount = value.DoctorCount,
                MonthlyAppointmentCount = value.MonthlyAppointmentCount,
                BedCount = value.BedCount,
                Raiting = value.Raiting,
                OccupancyRate = value.OccupancyRate,
                ThemeColour = value.ThemeColour
            };
            return View(updateBranchDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBranch(UpdateBranchDto updateBranchDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateBranchDto);
            }
            await _branchService.UpdateAsync(updateBranchDto);
            TempData["Success"] = "Şöbə məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }
    }
}
