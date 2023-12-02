using Repository;
using WebAPI.Services;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<MadiContext>();
            builder.Services.AddScoped<AirlineService>();
            builder.Services.AddCors();

            var app = builder.Build();

            using (var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<MadiContext>())
            {
                db.Database.EnsureCreated();
            }

            app.UseCors(builder => builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.MapControllers();

            app.Run();
        }
    }
}
