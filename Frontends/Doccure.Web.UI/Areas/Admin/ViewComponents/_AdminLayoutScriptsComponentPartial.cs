using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
namespace Doccure.Web.UI.Areas.Admin.ViewComponents
{
    public class _AdminLayoutScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
