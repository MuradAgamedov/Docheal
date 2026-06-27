
using Doccure.AppointmentService.Context;
using Doccure.AppointmentService.Services;

namespace Doccure.AppointmentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddScoped<
               Doccure.AppointmentService.Services.AppointmentServices.IAppointmentService,
               Doccure.AppointmentService.Services.AppointmentServices.AppointmentService>();


            builder.Services.AddScoped<
               Doccure.AppointmentService.Services.AppointmentDetailServices.IAppointmentDetailService,
               Doccure.AppointmentService.Services.AppointmentDetailServices.AppointmentDetailService>();
            builder.Services.AddDbContext<AppointmentContext>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
