using JobApplicationTrackerAPI.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // DbContext
            builder.Services.AddDbContext<JobApplicationTrackerDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure HTTP request pipeline for Development environments
            if (app.Environment.IsDevelopment())
            {
                // Generates the /openapi/v1.json spec document
                app.MapOpenApi();

                // Hosts the interactive Swagger UI interface pointing to native OpenAPI spec
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                    options.RoutePrefix = "swagger"; // Available at http://localhost:<port>/swagger
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
