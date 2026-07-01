
using Microsoft.EntityFrameworkCore;
using OrderService.Context;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<OrderContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")
                    )));

            builder.Services.AddAutoMapper(typeof(OrderService.Mapping.GeneralMapping));
            builder.Services.AddScoped<OrderService.Services.OrderServices.IOrderService, OrderService.Services.OrderServices.OrderService>();
            builder.Services.AddScoped<OrderService.Services.OrderDetailServices.IOrderDetailService, OrderService.Services.OrderDetailServices.OrderDetailService>();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
