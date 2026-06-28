using Doccure.Web.UI.Services.RegisterServices;

namespace Doccure.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<
                Doccure.Web.UI.Services.RegisterServices.IRegisterService,
                Doccure.Web.UI.Services.RegisterServices.RegisterService>();
            builder.Services.AddScoped<
             Doccure.Web.UI.Services.BranchServices.IBranchService,
             Doccure.Web.UI.Services.BranchServices.BranchService>();
            builder.Services.AddHttpClient<IRegisterService, RegisterService>();
            builder.Services.AddHttpClient<Doccure.Web.UI.Services.LoginServices.ILoginService, Doccure.Web.UI.Services.LoginServices.LoginService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

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
