
using AvanadeEstacionamento.API.Configuration;
using AvanadeEstacionamento.API.Middlewares;
using AvanadeEstacionamento.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AvanadeEstacionamentoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicione configuração para ler appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json");

            // Adicione configuração para ler appsettings.local.json, se existir
            if (File.Exists("appsettings.local.json"))
            {
                builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.ResolveDependencies();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}