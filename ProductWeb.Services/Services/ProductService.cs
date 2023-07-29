using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductWeb.Domain.Contexts;
using ProductWeb.Domain.Entities;
using ProductWeb.Domain.Models;
using ProductWeb.Services.Interfaces;

namespace ProductWeb.Services.Services;

public class ProductService : IProductService
{
    private ProductWebDbContext ProductWebDbContext { get; }
    
    private IMapper Mapper { get; }

    public ProductService(ProductWebDbContext productWebDbContext, IMapper mapper)
    {
        ProductWebDbContext = productWebDbContext;
        Mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts(CancellationToken cancellationToken)
    {
        var products = await ProductWebDbContext.Products
            .AsNoTracking()
            .Select(x => Mapper.Map<ProductDto>(x))
            .ToListAsync(cancellationToken);

        return products;
    }
    
    public async Task<ProductDto> GetProductById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
            
        }

        var product = await ProductWebDbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        var result = Mapper.Map<ProductDto>(product);

        return result;
    }

    public async Task<bool> Create(ProductDto productDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(productDto.Name))
        {
            throw new ArgumentException();
        }

        var category = await ProductWebDbContext.Categories.FindAsync(productDto.CategoryId);

        if (category == null)
        {
            throw new Exception("there is no category");
        }
        
        var newProduct = new ProductEntity
        {
            Name = productDto.Name,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId
        };

        ProductWebDbContext.Products.Add(newProduct);

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Update(int id, ProductDto productDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var productToUpdate = await ProductWebDbContext.Products
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (productToUpdate is null)
        {
            throw new NullReferenceException();
        }

        productToUpdate.Name = productDto.Name;
        productToUpdate.Price = productDto.Price;
        productToUpdate.CategoryId = productDto.CategoryId;

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var productToDelete = await ProductWebDbContext.Products
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (productToDelete is null)
        {
            throw new NullReferenceException();
        }

        ProductWebDbContext.Products.Remove(productToDelete);

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}