using Doccure.Web.UI.Dtos.CartDtos;

namespace Doccure.Web.UI.Services.CartServices
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCartAsync(string userId);
        Task AddToCartAsync(string userId, CartItemDto item);
        Task RemoveItemAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}
