
using Microsoft.EntityFrameworkCore;

namespace TPDojoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string? connectionStringHelloWorldDbContext = builder.Configuration.GetConnectionString(nameof(HelloWorldDbContext));
            builder.Services.AddDbContext<IHelloWorldDbContext, HelloWorldDbContext>(options =>
            {
                options.UseSqlServer(connectionStringHelloWorldDbContext);
            });

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
