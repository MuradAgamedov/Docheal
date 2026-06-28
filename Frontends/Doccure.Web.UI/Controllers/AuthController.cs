using Doccure.Web.UI.Models;
using Doccure.Web.UI.Services.RegisterServices;
using Doccure.Web.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly Doccure.Web.UI.Services.LoginServices.ILoginService _loginService;

        public AuthController(
            IRegisterService registerService, 
            Doccure.Web.UI.Services.LoginServices.ILoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _loginService.LoginAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("", "E-poçt və ya şifrə yanlışdır!");
                TempData["Error"] = "Məlumatlar yanlışdır!";
                return View(model);
            }

            TempData["Success"] = "Uğurla daxil oldunuz!";
            ViewBag.v = result;
            HttpContext.Session.SetString("JwtToken", result);
            return RedirectToAction("Index", "AdminLayaut", new { area = "Admin"});
        }
    }
}
