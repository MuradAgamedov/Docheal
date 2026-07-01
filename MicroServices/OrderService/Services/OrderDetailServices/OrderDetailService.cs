using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.Dtos.OrderDetailDtos;
using OrderService.Entities;

namespace OrderService.Services.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly OrderContext _context;
        private readonly IMapper _mapper;

        public OrderDetailService(OrderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultOrderDetailDto>> GetAllByOrderIdAsync(int orderId)
        {
            var details = await _context.OrderDetails
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
            return _mapper.Map<List<ResultOrderDetailDto>>(details);
        }

        public async Task<GetByIdOrderDetailDto?> GetOrderDetailByIdAsync(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail == null)
                return null;

            return _mapper.Map<GetByIdOrderDetailDto>(detail);
        }

        public async Task CreateOrderDetailAsync(CreateOrderDetailDto dto, int orderId)
        {
            var detail = _mapper.Map<OrderDetail>(dto);
            detail.OrderId = orderId;
            detail.TotalPrice = dto.UnitPrice * dto.Quantity;
            await _context.OrderDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
