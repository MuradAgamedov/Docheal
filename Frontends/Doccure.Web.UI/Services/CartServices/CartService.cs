using Doccure.Web.UI.Dtos.CartDtos;
using Newtonsoft.Json;

namespace Doccure.Web.UI.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:5000/api/cart";

        public CartService(HttpClient client) => _client = client;

        public async Task<List<CartItemDto>> GetCartAsync(string userId)
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}/{Uri.EscapeDataString(userId)}");
                if (!response.IsSuccessStatusCode) return new List<CartItemDto>();
                return JsonConvert.DeserializeObject<List<CartItemDto>>(await response.Content.ReadAsStringAsync())
                    ?? new List<CartItemDto>();
            }
            catch { return new List<CartItemDto>(); }
        }

        public async Task AddToCartAsync(string userId, CartItemDto item)
        {
            var payload = JsonConvert.SerializeObject(new
            {
                userId,
                item.ProductId,
                item.ProductName,
                item.Price,
                item.Quantity,
                item.ImageUrl
            });
            var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            await _client.PostAsync($"{BaseUrl}/add", content);
        }

        public async Task RemoveItemAsync(string userId, int productId)
        {
            await _client.DeleteAsync($"{BaseUrl}/remove?userId={Uri.EscapeDataString(userId)}&productId={productId}");
        }

        public async Task ClearCartAsync(string userId)
        {
            await _client.DeleteAsync($"{BaseUrl}/clear/{Uri.EscapeDataString(userId)}");
        }
    }
}
