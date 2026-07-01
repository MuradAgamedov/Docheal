using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.Dtos.OrderDtos;
using OrderService.Entities;

namespace OrderService.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;
        private readonly IMapper _mapper;

        public OrderService(OrderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<List<ResultOrderDto>>(orders);
        }

        public async Task<GetByIdOrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return null;

            return _mapper.Map<GetByIdOrderDto>(order);
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = dto.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order != null)
            {
                _mapper.Map(dto, order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
