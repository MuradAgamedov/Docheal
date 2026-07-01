using Doccure.Web.UI.Dtos.OrderDtos;

namespace Doccure.Web.UI.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<ResultOrderDto>> GetAllAsync();
        Task<GetByIdOrderDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateOrderDto dto);
        Task UpdateAsync(UpdateOrderDto dto);
        Task DeleteAsync(int id);
    }
}
