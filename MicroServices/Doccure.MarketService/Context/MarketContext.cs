using Doccure.MarketService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doccure.MarketService.Context
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions<MarketContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}