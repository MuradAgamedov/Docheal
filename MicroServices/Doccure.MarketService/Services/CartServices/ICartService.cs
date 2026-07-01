using Doccure.MarketService.Dtos.CartDtos;

namespace Doccure.MarketService.Services.CartServices
{
    public interface ICartService
    {
        Task AddItemToCartAsync(string userId, CartItemDto item);
        Task<List<CartItemDto>> GetCartAsync(string userId);
        Task RemoveItemAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}
