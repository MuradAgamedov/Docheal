using Doccure.Web.UI.ViewModels.Auth;
using System.Text;
using System.Text.Json;

namespace Doccure.Web.UI.Services.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _client;

        public RegisterService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7186/api/Auth/register", content);
            return response.IsSuccessStatusCode;
        }
    }
}
