using Doccure.Web.UI.ViewModels.Auth;
using System.Text;
using System.Text.Json;

namespace Doccure.Web.UI.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _client;

        public LoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var loginDto = new
            {
                Email = model.UsernameOrEmail,
                Password = model.Password
            };
            var json = JsonSerializer.Serialize(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7186/api/Auth/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var responseData = await response.Content.ReadAsStringAsync();
            var jsonDoc = Newtonsoft.Json.Linq.JObject.Parse(responseData);
            var token = jsonDoc["token"]?.ToString();
            return token;
        }
    }
}
