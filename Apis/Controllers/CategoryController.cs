using Apis.Models;
using Apis.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
    {
        var categories = await _categoryRepository.GetAll();
        if (categories == null) return NotFound("Category Empty");
        var result = categories.Select(x => CategoryDTO(x)).ToList();
        return Ok(result);
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryRepository.Get(id);

        if (category == null)
        {
            return NotFound();
        }

        return CategoryDTO(category);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CategoryDTO categoryDTO)
    {
        var category = new Category
        {
            Name = categoryDTO.Name,
            Description = categoryDTO.Description,
        };

        _categoryRepository.Create(category);
        await _categoryRepository.SaveAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = category.Id },
            CategoryDTO(category));
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id)
        {
            return BadRequest();
        }

        var category = await _categoryRepository.Get(id);

        if (category == null) return NotFound();


        category.Name = categoryDTO.Name;
        category.Description = categoryDTO.Description;

        try
        {
            await _categoryRepository.SaveAsync();
        }
        catch (DbUpdateConcurrencyException) when (!CategoryExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.Get(id);

        if (category == null) return NotFound();

        _categoryRepository.Delete(id);
        await _categoryRepository.SaveAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _categoryRepository.IsExisted(id);
    }

    private static CategoryDTO CategoryDTO(Category category) =>
        new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
}
