using Doccure.Web.UI.Dtos.DoctorDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly HttpClient _client;

        public DoctorService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(CreateDoctorDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync("https://localhost:5000/api/doctors", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:5000/api/doctors?id={id}");
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<List<ResultDoctorDto>> GetAllAsync()
        {
            var responseMessage = await _client.GetAsync("https://localhost:5000/api/doctors");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultDoctorDto>>(jsonData) ?? new List<ResultDoctorDto>();
        }

        public async Task<GetDoctorByIdDto> GetByIdAsync(string id)
        {
            var responseMessage = await _client.GetAsync($"https://localhost:5000/api/doctors/GetDoctor?id={id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetDoctorByIdDto>(jsonData);
        }

        public async Task UpdateAsync(UpdateDoctorDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync("https://localhost:5000/api/doctors", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}
