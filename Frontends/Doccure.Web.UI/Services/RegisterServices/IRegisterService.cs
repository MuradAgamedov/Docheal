using Doccure.Web.UI.ViewModels.Auth;

namespace Doccure.Web.UI.Services.RegisterServices
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
    }
}
