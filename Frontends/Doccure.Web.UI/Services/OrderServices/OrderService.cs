using Doccure.Web.UI.Dtos.OrderDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ResultOrderDto>> GetAllAsync()
        {
            try
            {
                var responseMessage = await _client.GetAsync("https://localhost:5000/api/orders");
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultOrderDto>>(jsonData) ?? new List<ResultOrderDto>();
            }
            catch (Exception)
            {
                return new List<ResultOrderDto>();
            }
        }

        public async Task<GetByIdOrderDto?> GetByIdAsync(int id)
        {
            try
            {
                var responseMessage = await _client.GetAsync($"https://localhost:5000/api/orders/{id}");
                if (!responseMessage.IsSuccessStatusCode)
                    return null;
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetByIdOrderDto>(jsonData);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task CreateAsync(CreateOrderDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync("https://localhost:5000/api/orders", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateOrderDto dto)
        {
            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync("https://localhost:5000/api/orders", stringContent);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var responseMessage = await _client.DeleteAsync($"https://localhost:5000/api/orders?id={id}");
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}
