using AutoMapper;
using Doccure.MarketService.Context;
using Doccure.MarketService.Dtos.ProductDtos;
using Doccure.MarketService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.MarketService.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly MarketContext _context;
        private readonly IMapper _mapper;

        public ProductService(MarketContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(products);
        }

        public async Task<GetByIdProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            return _mapper.Map<GetByIdProductDto>(product);
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product != null)
            {
                _mapper.Map(dto, product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
