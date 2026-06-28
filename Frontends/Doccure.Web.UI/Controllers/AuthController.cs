using Doccure.Web.UI.Models;
using Doccure.Web.UI.Services.RegisterServices;
using Doccure.Web.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IRegisterService _registerService;

        public AuthController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }
                await _registerService.RegisterAsync(model);
            TempData["Success"] = "Qeydiyyat uğurla yekunlaşmışdır!";
            return RedirectToAction("Register", "Auth");
        }
    }
}
