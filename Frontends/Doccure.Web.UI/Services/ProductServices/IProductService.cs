using Doccure.Web.UI.Dtos.ProductDtos;

namespace Doccure.Web.UI.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<ResultProductDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id);
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
