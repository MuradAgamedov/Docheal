using OrderService.Dtos.OrderDetailDtos;

namespace OrderService.Services.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task<List<ResultOrderDetailDto>> GetAllByOrderIdAsync(int orderId);
        Task<GetByIdOrderDetailDto?> GetOrderDetailByIdAsync(int id);
        Task CreateOrderDetailAsync(CreateOrderDetailDto dto, int orderId);
        Task DeleteOrderDetailAsync(int id);
    }
}
