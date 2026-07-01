using Doccure.Web.UI.Dtos.ProductDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Doccure.Web.UI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:5000/api/products";

        public ProductService(HttpClient client) => _client = client;

        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync(BaseUrl);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultProductDto>>(json) ?? new List<ResultProductDto>();
            }
            catch { return new List<ResultProductDto>(); }
        }

        public async Task<ResultProductDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}/{id}");
                if (!response.IsSuccessStatusCode) return null;
                return JsonConvert.DeserializeObject<ResultProductDto>(await response.Content.ReadAsStringAsync());
            }
            catch { return null; }
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            (await _client.PostAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            (await _client.PutAsync(BaseUrl, content)).EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            (await _client.DeleteAsync($"{BaseUrl}/{id}")).EnsureSuccessStatusCode();
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
                var response = await _client.PostAsync($"{BaseUrl}/upload-image", content);
                if (!response.IsSuccessStatusCode) return null;
                var obj = JObject.Parse(await response.Content.ReadAsStringAsync());
                return obj["url"]?.ToString();
            }
            catch { return null; }
        }
    }
}
