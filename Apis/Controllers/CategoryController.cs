using Apis.Models;
using Apis.Repository;
using Microsoft.AspNetCore.Mvc;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAll();
        if (categories == null) return NotFound("Category Empty");
        var result = categories.Select(x => CategoryDTO(x)).ToList();
        return Ok(result);
    }

    private static CategoryDTO CategoryDTO(Category category) =>
        new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
}
