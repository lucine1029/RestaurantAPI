
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Constants;
using RestaurantAPI.Data;
using RestaurantAPI.Data.Repositories;
using RestaurantAPI.Data.Repositories.IRepositories;
using RestaurantAPI.Services;
using RestaurantAPI.Services.IServices;

namespace RestaurantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.Configure<ResturantConfig>(builder.Configuration.GetSection("RestaurantConfiguration"));

            builder.Services.AddScoped<ITableRepo, TableRepo>();
            builder.Services.AddScoped<ITableService, TableService>();

            builder.Services.AddScoped<IMenuRepo, MenuRepo>();
            builder.Services.AddScoped<IMenuService, MenuService>();

            builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<IAdminRepo,AdminRepo>();
            builder.Services.AddScoped<IAdminService,AdminService>();

            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddScoped<IBookingService, BookingService>();


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
