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
            var values = JsonConvert.DeserializeObject<List<ResultBranchDto>>(jsonData);
            return values;
        }

        public Task<GetBranchByIdDto> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateBranchDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
