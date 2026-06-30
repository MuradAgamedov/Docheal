using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/401")]
        public IActionResult Error401()
        {
            return View();
        }

        [Route("Error/403")]
        public IActionResult Error403()
        {
            return View();
        }

        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("Error/503")]
        public IActionResult Error503()
        {
            return View();
        }
    }
}
