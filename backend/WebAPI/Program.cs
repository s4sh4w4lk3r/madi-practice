using Repository;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<MadiContext>();
            builder.Services.AddCors();

            var app = builder.Build();

            app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
            app.MapControllers();

            app.Run();
        }
    }
}
