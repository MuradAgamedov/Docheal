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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
                return RedirectToAction("Index", "AdminLayaut", new { area = "Admin" });
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

            var userId = ExtractClaim(result, "sub") ?? ExtractClaim(result, "nameid")
                ?? ExtractClaim(result, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                ?? model.UsernameOrEmail;
            HttpContext.Session.SetString("UserId", userId);

            return RedirectToAction("Index", "AdminLayaut", new { area = "Admin"});
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        private static string? ExtractClaim(string token, string claimKey)
        {
            try
            {
                var payload = token.Split('.')[1];
                var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
                var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                return obj[claimKey]?.ToString();
            }
            catch { return null; }
        }
    }
}
