using Doccure.Web.UI.Filters;
using Doccure.Web.UI.Handlers;
using Doccure.Web.UI.Services.RegisterServices;

namespace Doccure.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options=>
            {
                options.Filters.Add<ApiExceptionFilter>();
                options.Filters.Add<Doccure.Web.UI.Filters.SessionAuthFilter>();
            });
            builder.Services.AddHttpContextAccessor();
        
            builder.Services.AddScoped<
                Doccure.Web.UI.Services.RegisterServices.IRegisterService,
                Doccure.Web.UI.Services.RegisterServices.RegisterService>();
                builder.Services.AddTransient<JwtDelegatingHandler>();
            builder.Services.AddHttpClient<
             Doccure.Web.UI.Services.BranchServices.IBranchService,
             Doccure.Web.UI.Services.BranchServices.BranchService>()
                .AddHttpMessageHandler<JwtDelegatingHandler>();
            builder.Services.AddHttpClient<IRegisterService, RegisterService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.LoginServices.ILoginService, Doccure.Web.UI.Services.LoginServices.LoginService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.DoctorServices.IDoctorService, Doccure.Web.UI.Services.DoctorServices.DoctorService>()
                .AddHttpMessageHandler<JwtDelegatingHandler>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.PatientServices.IPatientService, Doccure.Web.UI.Services.PatientServices.PatientService>()
                .AddHttpMessageHandler<JwtDelegatingHandler>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.QueueServices.IQueueService, Doccure.Web.UI.Services.QueueServices.QueueService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.MedicineServices.IMedicineService, Doccure.Web.UI.Services.MedicineServices.MedicineService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.OrderServices.IOrderService, Doccure.Web.UI.Services.OrderServices.OrderService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.AppointmentServices.IAppointmentService, Doccure.Web.UI.Services.AppointmentServices.AppointmentService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.NurseServices.INurseService, Doccure.Web.UI.Services.NurseServices.NurseService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.ProductServices.IProductService, Doccure.Web.UI.Services.ProductServices.ProductService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.CartServices.ICartService, Doccure.Web.UI.Services.CartServices.CartService>();
     
            builder.Services.AddSession();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseStaticFiles();   // wwwroot/ (uploads, css, js və s.)

            var backendProductsPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "MicroServices", "Doccure.MarketService", "wwwroot", "uploads", "products"));
            if (!System.IO.Directory.Exists(backendProductsPath))
                System.IO.Directory.CreateDirectory(backendProductsPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(backendProductsPath),
                RequestPath = "/uploads/products"
            });

            var backendMedicinesPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "MicroServices", "Doccure.PharmacyService", "wwwroot", "uploads", "medicines"));
            if (!System.IO.Directory.Exists(backendMedicinesPath))
                System.IO.Directory.CreateDirectory(backendMedicinesPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(backendMedicinesPath),
                RequestPath = "/uploads/medicines"
            });

            var backendPatientsPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "MicroServices", "Doccure.PatientService", "wwwroot", "uploads", "patients"));
            if (!System.IO.Directory.Exists(backendPatientsPath))
            {
                System.IO.Directory.CreateDirectory(backendPatientsPath);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(backendPatientsPath),
                RequestPath = "/uploads/patients"
            });

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
