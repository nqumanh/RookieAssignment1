using AutoMapper;
using Apis.Models;
using Apis.Repository;
using SharedViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(IGenericRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
    {
        var categories = await _categoryRepository.Entities.ToListAsync();
        if (categories == null)
            return NotFound();
        var result = _mapper.Map<List<CategoryDTO>>(categories);
        return Ok(result);
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        return _mapper.Map<CategoryDTO>(category);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CategoryDTO categoryDTO)
    {
        var category = new Category
        {
            Name = categoryDTO.Name,
            Description = categoryDTO.Description,
        };

        await _categoryRepository.AddAsync(category);

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

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null) return NotFound();

        category.Name = categoryDTO.Name;
        category.Description = categoryDTO.Description;

        await _categoryRepository.UpdateAsync(category);

        return NoContent();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null) return NotFound();

        await _categoryRepository.DeleteAsync(category);

        return NoContent();
    }
}
