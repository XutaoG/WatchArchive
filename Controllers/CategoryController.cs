using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Extensions;
using WatchArchive.Server.Models;
using WatchArchive.Server.Repositories.CategoryRepo;

namespace WatchArchive.Server.Controllers;

[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllByUserId()
    {
        Guid userId = User.GetUserId();

        List<Category> categories = await categoryRepository.GetByUserId((Guid)userId);
        List<CategoryResponse> res = mapper.Map<List<CategoryResponse>>(categories);

        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        Category? foundCategory = await categoryRepository.GetById(id);
        if (foundCategory == null || foundCategory.UserId != userId)
        {
            return NotFound();
        }

        CategoryResponse res = mapper.Map<CategoryResponse>(foundCategory);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest req)
    {
        Guid userId = User.GetUserId();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Category newCategory = mapper.Map<Category>(req);
        newCategory.UserId = userId;

        Category foundCategory = await categoryRepository.Create(newCategory);
        CategoryResponse res = mapper.Map<CategoryResponse>(foundCategory);

        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest req
    )
    {
        Guid userId = User.GetUserId();

        Category? foundCategory = await categoryRepository.GetById(id);
        if (foundCategory == null || foundCategory.UserId != userId)
        {
            return NotFound();
        }

        Category newCategory = mapper.Map<Category>(req);
        Category? updatedCategory = await categoryRepository.UpdateById(id, newCategory);
        if (updatedCategory == null)
        {
            return NotFound();
        }

        CategoryResponse res = mapper.Map<CategoryResponse>(updatedCategory);

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        Category? foundCategory = await categoryRepository.GetById(id);
        if (foundCategory == null || foundCategory.UserId != userId)
        {
            return NotFound();
        }

        await categoryRepository.DeleteById(id);
        return NoContent();
    }
}
