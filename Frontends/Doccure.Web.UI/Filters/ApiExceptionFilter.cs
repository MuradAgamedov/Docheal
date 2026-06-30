using Doccure.Web.UI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doccure.Web.UI.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                context.Result = new RedirectResult($"/Error/{apiException.StatusCode}");
                context.ExceptionHandled = true;
            }
            else if (context.Exception is HttpRequestException)
            {
                context.Result = new RedirectResult("/Error/503");
                context.ExceptionHandled = true;
            }
        }
    }
}
