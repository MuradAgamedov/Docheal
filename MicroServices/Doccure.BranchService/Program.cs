
using Doccure.BranchService.Services;
using Doccure.BranchService.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

namespace Doccure.BranchService
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
                Doccure.BranchService.Services.IBranchService,
                Doccure.BranchService.Services.BranchService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));

            builder.Services.AddSingleton<IDatabaseSettings>(sp=>sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            builder.Services.AddEndpointsApiExplorer();
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
