
using HomeWebAp.Api.Controllers;
using HomeWebApp.Application;
using HomeWebApp.Persistence;
using HomeWebApp.Infrastructure;

namespace HomeWebAp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiService(builder.Configuration)
                            .AddApplicationService(builder.Environment.WebRootPath)
                            .AddPersistenceService(builder.Configuration)
                            .AddInfrastructureService(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            

          /*  app.Use(async (HttpContext context,RequestDelegate next) =>
            {
                 await   context.Response.WriteAsync("This Is Continous Middleware");
                 await next(context);
            });

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("This is Second Middleware");
                await next(context);
            });
            app.Run(async (HttpContext context) =>
            {
                await context.Response.WriteAsync("This is Map Middleware");
            });*/

            app.UseHttpsRedirection();

            app.UseAuthorization();
           

            app.MapControllers();

            app.Run();
        }
    }
}