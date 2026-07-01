using Doccure.MarketService.Dtos.ProductDtos;

namespace Doccure.MarketService.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductsAsync();
        Task<GetByIdProductDto?> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductDto dto);
        Task UpdateProductAsync(UpdateProductDto dto);
        Task DeleteProductAsync(int id);
    }
}
