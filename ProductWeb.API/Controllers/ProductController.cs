using Microsoft.AspNetCore.Mvc;
using ProductWeb.Domain.Models;
using ProductWeb.Services.Interfaces;

namespace ProductWebAPI.Controllers;
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService ProductService;

    public ProductController(IProductService productService)
    {
        ProductService = productService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var result = await ProductService.GetAllProducts(cancellationToken);

        return Ok(result);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var result = await ProductService.GetProductById(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost("products")]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        var result = await ProductService.Create(productDto, cancellationToken);

        return Ok(result);
    }

    [HttpPost("products/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        var result = await ProductService.Update(id, productDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await ProductService.Delete(id, cancellationToken);

        return Ok(result);
    }
}