
using APIConsumer.Interfaces;
using APIConsumer.Tools;
using CarStockAPI.Repositories;

namespace CarStockAPI1
{
    public class Program
    {
        public static IConfiguration _configuration;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Program._configuration = new ConfigurationBuilder()
                                                                .AddJsonFile("appsettings.json")
                                                                .AddEnvironmentVariables()
                                                                .Build();

            builder.Services.AddScoped<IAPIConnector, APIConnector>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
