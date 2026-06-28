using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.ViewComponents
{
    public class _AdminLayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() {  return View(); }
    }
}
