using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayautController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
