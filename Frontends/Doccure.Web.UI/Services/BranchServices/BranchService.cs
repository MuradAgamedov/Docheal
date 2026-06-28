using Doccure.Web.UI.Dtos.BranchDtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Doccure.Web.UI.Services.BranchServices
{
    public class BranchService : IBranchService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BranchService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(CreateBranchDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync("https://localhost:5000/api/branches", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:5000/api/branches?id={id}");
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<List<ResultBranchDto>> GetAllAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _client.GetAsync("https://localhost:5000/api/branches");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBranchDto>>(jsonData);
            return values;
        }

        public async Task<GetBranchByIdDto> GetByIdAsync(string id)
        {
            var responseMessage = await _client.GetAsync($"https://localhost:5000/api/branches/GetBranch?id={id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetBranchByIdDto>(jsonData);
            return value;
        }

        public async Task UpdateAsync(UpdateBranchDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync("https://localhost:5000/api/branches", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}
