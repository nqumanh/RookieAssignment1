using Apis.Models;
using Apis.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
    {
        var categories = await _categoryRepository.GetAll();
        if (categories == null) return NotFound("Category Empty");
        var result = _mapper.Map<List<CategoryDTO>>(categories);
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

        return _mapper.Map<CategoryDTO>(category);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CategoryDTO categoryDTO)
    {
        if (categoryDTO.Name == null)
        {
            return BadRequest("Name of category is required!");
        }
        
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
            _mapper.Map<CategoryDTO>(category));
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
}
