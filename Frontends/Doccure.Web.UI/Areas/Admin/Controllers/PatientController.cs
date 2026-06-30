using Doccure.Web.UI.Dtos.PatientDtos;
using Doccure.Web.UI.Services.PatientServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _patientService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientDto createPatientDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(createPatientDto);

            try
            {
                createPatientDto.ImageFile = imageFile;

                await _patientService.CreateAsync(createPatientDto);

                TempData["Success"] = "Xəstə uğurla əlavə edildi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(createPatientDto);
            }
        }

        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientService.DeleteAsync(id);
            TempData["Success"] = "Xəstə uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePatient(int id)
        {
            var value = await _patientService.GetByIdAsync(id);

            if (value == null)
            {
                TempData["Error"] = "Xəstə tapılmadı!";
                return RedirectToAction("Index");
            }

            var updatePatientDto = new UpdatePatientDto
            {
                PatientId = value.PatientId,
                AppUserId = value.AppUserId,

                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                PhoneNumber = value.PhoneNumber,
                Gender = value.Gender,
                BloodGroup = value.BloodGroup,
                BirthDate = value.BirthDate,
                City = value.City,
                ImageUrl = value.ImageUrl,

                TcKimlikNo = value.TcKimlikNo,
                InsuranceType = value.InsuranceType,
                Status = value.Status
            };

            return View(updatePatientDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatient(UpdatePatientDto updatePatientDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(updatePatientDto);

            try
            {
                updatePatientDto.ImageFile = imageFile;

                await _patientService.UpdateAsync(updatePatientDto);

                TempData["Success"] = "Xəstə məlumatları uğurla yeniləndi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(updatePatientDto);
            }
        }
    }
}