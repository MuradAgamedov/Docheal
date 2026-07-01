using Doccure.Web.UI.Dtos.NurseDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.NurseServices
{
    public class NurseService : INurseService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:5000/api/nurses";

        public NurseService(HttpClient client) => _client = client;

        public async Task<List<ResultNurseDto>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync(BaseUrl);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultNurseDto>>(json) ?? new List<ResultNurseDto>();
            }
            catch { return new List<ResultNurseDto>(); }
        }

        public async Task<GetByIdNurseDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}/{id}");
                if (!response.IsSuccessStatusCode) return null;
                return JsonConvert.DeserializeObject<GetByIdNurseDto>(await response.Content.ReadAsStringAsync());
            }
            catch { return null; }
        }

        public async Task CreateAsync(CreateNurseDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            (await _client.PostAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateNurseDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            (await _client.PutAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            (await _client.DeleteAsync($"{BaseUrl}?id={id}")).EnsureSuccessStatusCode();
        }
    }
}
