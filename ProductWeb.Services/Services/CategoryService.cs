using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductWeb.Domain.Contexts;
using ProductWeb.Domain.Entities;
using ProductWeb.Domain.Models;
using ProductWeb.Services.Interfaces;

namespace ProductWeb.Services.Services;

public class CategoryService : ICategoryService
{
    private ProductWebDbContext ProductWebDbContext { get; }
    
    private IMapper Mapper { get; }

    public CategoryService(ProductWebDbContext productWebDbContext, IMapper mapper)
    {
        ProductWebDbContext = productWebDbContext;
        Mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
    {
        var categories = await ProductWebDbContext.Categories
            .AsNoTracking()
            .Select(x => Mapper.Map<CategoryDto>(x))
            .ToListAsync(cancellationToken);

        return categories;
    }
    
    public async Task<CategoryDto> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
            
        }

        var category = await ProductWebDbContext.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        var result = Mapper.Map<CategoryDto>(category);

        return result;
    }
    
    public async Task<bool> Create(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(categoryDto.Name))
        {
            throw new ArgumentException();
        }

        var newCategory = new CategoryEntity
        {
            Name = categoryDto.Name
        };

        ProductWebDbContext.Categories.Add(newCategory);

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Update(int id, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var categoryToUpdate = await ProductWebDbContext.Categories
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (categoryToUpdate is null)
        {
            throw new NullReferenceException();
        }

        categoryToUpdate.Name = categoryDto.Name ?? categoryToUpdate.Name;

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var categoryToDelete = await ProductWebDbContext.Categories
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (categoryToDelete is null)
        {
            throw new NullReferenceException();
        }

        ProductWebDbContext.Categories.Remove(categoryToDelete);

        await ProductWebDbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}