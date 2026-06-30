using Doccure.Web.UI.Dtos.BranchDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.BranchServices
{
    public class BranchService : IBranchService
    {
        private readonly HttpClient _client;

        public BranchService(HttpClient client)
        {
            _client = client;
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
            var responseMessage = await _client.GetAsync("https://localhost:5000/api/branches");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultBranchDto>>(jsonData) ?? new List<ResultBranchDto>();
        }

        public async Task<GetBranchByIdDto> GetByIdAsync(string id)
        {
            var responseMessage = await _client.GetAsync($"https://localhost:5000/api/branches/GetBranch?id={id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetBranchByIdDto>(jsonData);
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
