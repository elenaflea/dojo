using Microsoft.EntityFrameworkCore;

namespace TPDojoWeb
{
    public class Program
    {
        public static void Main(string[] args)
 
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            string? connectionStringHelloWorldDbContext = builder.Configuration.GetConnectionString(nameof(HelloWorldDbContext));
            builder.Services.AddDbContext<IHelloWorldDbContext, HelloWorldDbContext>(options =>
            {
                options.UseSqlServer(connectionStringHelloWorldDbContext);
            });

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
