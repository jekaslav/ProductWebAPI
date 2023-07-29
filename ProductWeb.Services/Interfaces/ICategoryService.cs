using ProductWeb.Domain.Models;

namespace ProductWeb.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);

    Task<CategoryDto> GetCategoryById(int id, CancellationToken cancellationToken);

    Task<bool> Create(CategoryDto categoryDto, CancellationToken cancellationToken);

    Task<bool> Update(int id, CategoryDto categoryDto, CancellationToken cancellationToken);

    Task<bool> Delete(int id, CancellationToken cancellationToken);
}