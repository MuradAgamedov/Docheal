using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.ViewComponents
{
    public class _AdminLayoutStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() { 
            return View();
        }
    }
}
