using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Extensions;
using WatchArchive.Server.Models;
using WatchArchive.Server.Repositories.TagRepo;

namespace WatchArchive.Server.Controllers;

[Route("api/tags")]
[ApiController]
[Authorize]
public class TagController(ITagRepository tagRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllByUserId()
    {
        Guid userId = User.GetUserId();

        List<Tag> tags = await tagRepository.GetAllByUserId(userId);
        List<TagResponse> res = mapper.Map<List<TagResponse>>(tags);

        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        Tag? foundTag = await tagRepository.GetByUserIdAndId(userId, id);
        if (foundTag == null)
        {
            return NotFound();
        }

        TagResponse res = mapper.Map<TagResponse>(foundTag);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTagRequest req)
    {
        Guid userId = User.GetUserId();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Tag newTag = mapper.Map<Tag>(req);
        newTag.UserId = userId;

        // Check if tag already exists
        bool exists = await tagRepository.ExistsByNameAndUserId(userId, newTag.Name);
        if (exists)
        {
            return Conflict();
        }

        // Add new tag
        Tag foundTag = await tagRepository.Create(newTag);
        TagResponse res = mapper.Map<TagResponse>(foundTag);

        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(
        [FromRoute] Guid id,
        [FromBody] UpdateTagRequest req
    )
    {
        Guid userId = User.GetUserId();

        Tag? foundTag = await tagRepository.GetByUserIdAndId(userId, id);
        if (foundTag == null)
        {
            return NotFound();
        }

        Tag newTag = mapper.Map<Tag>(req);
        Tag? updatedTag = await tagRepository.UpdateById(id, newTag);
        if (updatedTag == null)
        {
            return NotFound();
        }

        TagResponse res = mapper.Map<TagResponse>(updatedTag);

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        Tag? foundTag = await tagRepository.GetByUserIdAndId(userId, id);
        if (foundTag == null)
        {
            return NotFound();
        }

        await tagRepository.DeleteById(id);
        return NoContent();
    }
}
