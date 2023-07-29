using Microsoft.AspNetCore.Mvc;
using ProductWeb.Domain.Models;
using ProductWeb.Services.Interfaces;

namespace ProductWebAPI.Controllers;
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService CategoryService;

    public CategoryController(ICategoryService categoryService)
    {
        CategoryService = categoryService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await CategoryService.GetAllCategories(cancellationToken);

        return Ok(result);
    }

    [HttpGet("categories/{id}")]
    public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
    {
        var result = await CategoryService.GetCategoryById(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await CategoryService.Create(categoryDto, cancellationToken);

        return Ok(result);
    }

    [HttpPost("categories/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto,
        CancellationToken cancellationToken)
    {
        var result = await CategoryService.Update(id, categoryDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await CategoryService.Delete(id, cancellationToken);

        return Ok(result);
    }
}