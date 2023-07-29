using ProductWeb.Domain.Models;

namespace ProductWeb.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts(CancellationToken cancellationToken);

    Task<ProductDto> GetProductById(int id, CancellationToken cancellationToken);

    Task<bool> Create(ProductDto productDto, CancellationToken cancellationToken);

    Task<bool> Update(int id, ProductDto productDto, CancellationToken cancellationToken);

    Task<bool> Delete(int id, CancellationToken cancellationToken);
}