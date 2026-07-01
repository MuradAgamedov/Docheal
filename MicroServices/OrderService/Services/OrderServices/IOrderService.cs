using OrderService.Dtos.OrderDtos;

namespace OrderService.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<ResultOrderDto>> GetAllOrdersAsync();
        Task<GetByIdOrderDto?> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(CreateOrderDto dto);
        Task UpdateOrderAsync(UpdateOrderDto dto);
        Task DeleteOrderAsync(int id);
    }
}
