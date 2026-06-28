using Doccure.Web.UI.ViewModels.Auth;

namespace Doccure.Web.UI.Services.LoginServices
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginViewModel model);
    }
}
