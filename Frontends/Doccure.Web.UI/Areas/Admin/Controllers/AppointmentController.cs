using Doccure.Web.UI.Dtos.AppointmentDtos;
using Doccure.Web.UI.Services.AppointmentServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _appointmentService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAppointment() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _appointmentService.CreateAsync(dto);
            TempData["Success"] = "Görüş uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentService.DeleteAsync(id);
            TempData["Success"] = "Görüş uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAppointment(int id)
        {
            var value = await _appointmentService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Görüş tapılmadı!";
                return RedirectToAction("Index");
            }
            var dto = new UpdateAppointmentDto
            {
                AppointmentId = value.AppointmentId,
                AppointmentDate = value.AppointmentDate,
                Status = value.Status
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _appointmentService.UpdateAsync(dto);
            TempData["Success"] = "Görüş məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }
    }
}
