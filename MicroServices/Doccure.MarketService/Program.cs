using Doccure.MarketService.Context;
using Microsoft.EntityFrameworkCore;

namespace Doccure.MarketService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<Services.IRedisService, Services.RedisService>();
            builder.Services.AddDbContext<MarketContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(typeof(Doccure.MarketService.Mapping.GeneralMapping));
            builder.Services.AddScoped<Doccure.MarketService.Services.ProductServices.IProductService, Doccure.MarketService.Services.ProductServices.ProductService>();
            builder.Services.AddScoped<Doccure.MarketService.Services.CartServices.ICartService, Doccure.MarketService.Services.CartServices.CartService>();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
