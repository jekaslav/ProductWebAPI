using Microsoft.EntityFrameworkCore;
using ProductWeb.Domain.Entities;

namespace ProductWeb.Domain.Contexts;

public sealed class ProductWebDbContext : DbContext
{
    public ProductWebDbContext(DbContextOptions<ProductWebDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<CategoryEntity> Categories { get; set; } = null!;

    public DbSet<ProductEntity> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);
    }
}