using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doccure.Web.UI.Filters
{
    public class SessionAuthFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            if (descriptor == null) return;

            var controllerName = descriptor.ControllerName;
            var actionName = descriptor.ActionName;

            // Auth controller-i keç (Login, Register)
            if (controllerName.Equals("Auth", StringComparison.OrdinalIgnoreCase))
                return;

            // Error controller-i keç
            if (controllerName.Equals("Error", StringComparison.OrdinalIgnoreCase))
                return;

            var token = context.HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectResult("/Auth/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
