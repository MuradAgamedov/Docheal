using Doccure.Web.UI.Dtos.NurseDtos;
using Doccure.Web.UI.Services.NurseServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NurseController : Controller
    {
        private readonly INurseService _nurseService;

        public NurseController(INurseService nurseService) => _nurseService = nurseService;

        public async Task<IActionResult> Index()
        {
            var values = await _nurseService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateNurse() => View();

        [HttpPost]
        public async Task<IActionResult> CreateNurse(CreateNurseDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _nurseService.CreateAsync(dto);
            TempData["Success"] = "Tibb bacısı uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteNurse(int id)
        {
            await _nurseService.DeleteAsync(id);
            TempData["Success"] = "Tibb bacısı uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNurse(int id)
        {
            var value = await _nurseService.GetByIdAsync(id);
            if (value == null) { TempData["Error"] = "Tibb bacısı tapılmadı!"; return RedirectToAction("Index"); }
            var dto = new UpdateNurseDto
            {
                NurseId = value.NurseId, FirstName = value.FirstName, LastName = value.LastName,
                Unit = value.Unit, Branch = value.Branch, Shift = value.Shift, Status = value.Status,
                Experience = value.Experience, Skills = value.Skills, Performance = value.Performance, Attendance = value.Attendance
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNurse(UpdateNurseDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _nurseService.UpdateAsync(dto);
            TempData["Success"] = "Tibb bacısı məlumatları yeniləndi!";
            return RedirectToAction("Index");
        }
    }
}
