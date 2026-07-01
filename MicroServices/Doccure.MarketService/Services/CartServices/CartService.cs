using Doccure.MarketService.Dtos.CartDtos;
using System.Text.Json;

namespace Doccure.MarketService.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly IRedisService _redisService;

        public CartService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        private static string Key(string userId) => $"cart:{userId}";

        public async Task AddItemToCartAsync(string userId, CartItemDto dto)
        {
            var raw = await _redisService.GetValueAsync(Key(userId));
            var items = string.IsNullOrEmpty(raw)
                ? new List<CartItemDto>()
                : JsonSerializer.Deserialize<List<CartItemDto>>(raw) ?? new List<CartItemDto>();

            var existing = items.FirstOrDefault(x => x.ProductId == dto.ProductId);
            if (existing != null)
                existing.Quantity += dto.Quantity;
            else
                items.Add(dto);

            await _redisService.SetValueAsync(Key(userId), JsonSerializer.Serialize(items));
        }

        public async Task<List<CartItemDto>> GetCartAsync(string userId)
        {
            var raw = await _redisService.GetValueAsync(Key(userId));
            if (string.IsNullOrEmpty(raw)) return new List<CartItemDto>();
            return JsonSerializer.Deserialize<List<CartItemDto>>(raw) ?? new List<CartItemDto>();
        }

        public async Task RemoveItemAsync(string userId, int productId)
        {
            var raw = await _redisService.GetValueAsync(Key(userId));
            if (string.IsNullOrEmpty(raw)) return;

            var items = JsonSerializer.Deserialize<List<CartItemDto>>(raw) ?? new List<CartItemDto>();
            var item = items.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                items.Remove(item);
                await _redisService.SetValueAsync(Key(userId), JsonSerializer.Serialize(items));
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            await _redisService.DeleteKeyAsync(Key(userId));
        }
    }
}
