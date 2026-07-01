using Doccure.Web.UI.Dtos.AppointmentDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.AppointmentServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:5000/api/appointments";

        public AppointmentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ResultAppointmentDto>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync(BaseUrl);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultAppointmentDto>>(json) ?? new List<ResultAppointmentDto>();
            }
            catch
            {
                return new List<ResultAppointmentDto>();
            }
        }

        public async Task<GetByIdAppointmentDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}/GetAppointment?id={id}");
                if (!response.IsSuccessStatusCode) return null;
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetByIdAppointmentDto>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task CreateAsync(CreateAppointmentDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            (await _client.PostAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateAppointmentDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            (await _client.PutAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            (await _client.DeleteAsync($"{BaseUrl}?id={id}")).EnsureSuccessStatusCode();
        }
    }
}
