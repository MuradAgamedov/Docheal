using Doccure.Web.UI.Dtos.DoctorDtos;
using Doccure.Web.UI.Services.BranchServices;
using Doccure.Web.UI.Services.DoctorServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IBranchService _branchService;
        private readonly IWebHostEnvironment _env;

        public DoctorController(IDoctorService doctorService, IBranchService branchService, IWebHostEnvironment env)
        {
            _doctorService = doctorService;
            _branchService = branchService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _doctorService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDoctor()
        {
            ViewBag.Branches = await _branchService.GetAllAsync();
            return View(new CreateDoctorDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto createDoctorDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Branches = await _branchService.GetAllAsync();
                return View(createDoctorDto);
            }
            createDoctorDto.ImageUrl = await SaveImageAsync(imageFile);
            await _doctorService.CreateAsync(createDoctorDto);
            TempData["Success"] = "Həkim uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDoctor(string id)
        {
            await _doctorService.DeleteAsync(id);
            TempData["Success"] = "Həkim uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDoctor(string id)
        {
            var value = await _doctorService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Həkim tapılmadı!";
                return RedirectToAction("Index");
            }
            ViewBag.Branches = await _branchService.GetAllAsync();
            var updateDto = new UpdateDoctorDto
            {
                DoctorId = value.DoctorId,
                Name = value.Name,
                Surname = value.Surname,
                BranchId = value.BranchId,
                Email = value.Email,
                Phone = value.Phone,
                ImageUrl = value.ImageUrl,
                About = value.About,
                ExperienceYear = value.ExperienceYear,
                PricePerHour = value.PricePerHour,
                Educations = value.Educations ?? new List<EducationDto>(),
                Experiences = value.Experiences ?? new List<ExperienceDto>(),
                Awards = value.Awards ?? new List<AwardDto>(),
                Locations = value.Locations ?? new List<LocationDto>(),
                Services = value.Services ?? new List<string>(),
                Specializations = value.Specializations ?? new List<string>()
            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorDto updateDoctorDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Branches = await _branchService.GetAllAsync();
                return View(updateDoctorDto);
            }
            var savedPath = await SaveImageAsync(imageFile);
            if (savedPath != null)
                updateDoctorDto.ImageUrl = savedPath;
            await _doctorService.UpdateAsync(updateDoctorDto);
            TempData["Success"] = "Həkim məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }

        private async Task<string?> SaveImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadsDir = Path.Combine(webRoot, "uploads", "doctors");
            Directory.CreateDirectory(uploadsDir);
            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/doctors/{fileName}";
        }
    }
}
