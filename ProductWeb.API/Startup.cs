using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductWeb.Domain.Contexts;
using ProductWeb.Services.Interfaces;
using ProductWeb.Services.Mappers;
using ProductWeb.Services.Services;

namespace ProductWeb.API;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductWeb.API", Version = "v1" });
        });
        
        var connection = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddDbContext<ProductWebDbContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly("ProductWeb.API")));
            
        builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
                        
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();  
    }
        
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductWeb.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
    }
}