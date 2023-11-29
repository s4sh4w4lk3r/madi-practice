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

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
