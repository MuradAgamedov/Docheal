
using Doccure.Web.UI.Exceptions;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Doccure.Web.UI.Handlers
{
    public class JwtDelegatingHandler : DelegatingHandler 
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public JwtDelegatingHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _contextAccessor.HttpContext;
            if (httpContext != null)
            {
                var token = httpContext.Session.GetString("JwtToken");
                if (!string.IsNullOrEmpty(token))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new ApiException((int)response.StatusCode);

            return response;
        }
    }
}
