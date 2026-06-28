using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.ViewComponents
{
    public class _AdminLayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() { 
            return View();
        }
    }
}
