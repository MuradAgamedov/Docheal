using Doccure.Web.UI.Dtos.MedicineDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Doccure.Web.UI.Services.MedicineServices
{
    public class MedicineService : IMedicineService
    {
        private readonly HttpClient _client;

        public MedicineService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ResultMedicineDto>> GetAllAsync()
        {
            try
            {
                var responseMessage = await _client.GetAsync("https://localhost:5000/api/medicines");
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultMedicineDto>>(jsonData) ?? new List<ResultMedicineDto>();
            }
            catch (Exception)
            {
                return new List<ResultMedicineDto>();
            }
        }

        public async Task<ResultMedicineDto?> GetByIdAsync(int id)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"https://localhost:5000/api/medicines/{id}");
                if (!responseMessage.IsSuccessStatusCode)
                    return null;
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultMedicineDto>(jsonData);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task CreateAsync(CreateMedicineDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync("https://localhost:5000/api/medicines", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateMedicineDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync("https://localhost:5000/api/medicines", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:5000/api/medicines/{id}");
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                var response = await _client.PostAsync("https://localhost:5000/api/medicines/upload-image", content);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(json);
                return obj["url"]?.ToString();
            }
            catch { return null; }
        }
    }
}
