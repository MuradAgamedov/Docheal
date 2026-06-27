using Doccure.PrescriptionService.Context;
using Microsoft.EntityFrameworkCore;

namespace Doccure.PrescriptionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PrescriptionContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));
            builder.Services.AddControllers();
            builder.Services.AddScoped<
                   Doccure.PrescriptionService.Services.PrescriptionServices.IPrescriptionService,
                   Doccure.PrescriptionService.Services.PrescriptionServices.PrescriptionService>();


            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
