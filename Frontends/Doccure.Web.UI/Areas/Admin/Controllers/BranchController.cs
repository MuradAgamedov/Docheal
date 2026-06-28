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
    }
}
