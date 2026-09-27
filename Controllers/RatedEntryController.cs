using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchArchive.Server.DTOs.RequestDTOs;
using WatchArchive.Server.DTOs.ResponseDTOs;
using WatchArchive.Server.Extensions;
using WatchArchive.Server.Models;
using WatchArchive.Server.Repositories.CategoryRepo;
using WatchArchive.Server.Repositories.RatedEntryRepo;
using WatchArchive.Server.Repositories.TagRepo;

namespace WatchArchive.Server.Controllers;

[Route("api/rated_entries")]
[ApiController]
[Authorize]
public class RatedEntryController(
    IRatedEntryRepository ratedEntryRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllByUserId()
    {
        Guid userId = User.GetUserId();

        List<RatedEntry> entries = await ratedEntryRepository.GetAllByUserId(userId);
        List<RatedEntryResponse> res = mapper.Map<List<RatedEntryResponse>>(entries);

        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        RatedEntry? foundEntry = await ratedEntryRepository.GetByUserIdAndId(userId, id);
        if (foundEntry == null)
        {
            return NotFound();
        }

        RatedEntryResponse res = mapper.Map<RatedEntryResponse>(foundEntry);
        return Ok(res);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateRatedEntryRequest req)
    {
        Guid userId = User.GetUserId();

        CreateRatedEntryDataRequest? entryDataReq;
        CreateThumbnailDataRequest? thumbnailDataReq;

        // Deserialize JSON data

        JsonSerializerOptions jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        try
        {
            entryDataReq = JsonSerializer.Deserialize<CreateRatedEntryDataRequest>(
                req.EntryData,
                jsonSerializerOptions
            );
            thumbnailDataReq = JsonSerializer.Deserialize<CreateThumbnailDataRequest>(
                req.ThumbnailData,
                jsonSerializerOptions
            );
        }
        catch (JsonException)
        {
            return BadRequest("Invalid Json data");
        }

        if (entryDataReq == null || thumbnailDataReq == null)
        {
            return BadRequest("Invalid Json data");
        }

        // Validate JSON data
        if (!TryValidateModel(entryDataReq) || !TryValidateModel(thumbnailDataReq))
        {
            return ValidationProblem(ModelState);
        }

        // Sanitize data

        entryDataReq.Name = entryDataReq.Name.Trim();
        entryDataReq.OverallRating = Math.Round(entryDataReq.OverallRating * 2) / 2;

        // Validate id existence
        entryDataReq.CategoryIds = [.. entryDataReq.CategoryIds.Distinct()];
        List<Category> foundCategories = await categoryRepository.GetAllByUserIdAndIds(
            userId,
            entryDataReq.CategoryIds
        );
        if (entryDataReq.CategoryIds.Count != foundCategories.Count)
        {
            return BadRequest("One or more category IDs are invalid.");
        }

        entryDataReq.TagIds = [.. entryDataReq.TagIds.Distinct()];
        List<Tag> foundTags = await tagRepository.GetAllByUserIdAndIds(userId, entryDataReq.TagIds);
        if (entryDataReq.TagIds.Count != foundTags.Count)
        {
            return BadRequest("One or more tag IDs are invalid.");
        }

        // Create models
        List<CategoryEntry> newCategoryEntries =
        [
            .. entryDataReq.CategoryIds.Select(c => new CategoryEntry() { CategoryId = c }),
        ];
        List<TagEntry> newTagEntries =
        [
            .. entryDataReq.TagIds.Select(t => new TagEntry() { TagId = t }),
        ];

        Thumbnail newThumbnail = new()
        {
            UseAutoGenerate = thumbnailDataReq.UseAutoGenerate,
            AutoGenerateKeyword = thumbnailDataReq.AutoGenerateKeyword,
        };

        RatedEntry newEntry = new()
        {
            Name = entryDataReq.Name,
            OverallRating = entryDataReq.OverallRating,
            Thumbnail = newThumbnail,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            UserId = userId,
            CategoryEntries = newCategoryEntries,
            TagEntries = newTagEntries,
        };

        // Add new rated entry
        RatedEntry foundEntry = await ratedEntryRepository.Create(newEntry);

        RatedEntryResponse res = mapper.Map<RatedEntryResponse>(foundEntry);

        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        Guid userId = User.GetUserId();

        RatedEntry? foundRatedEntry = await ratedEntryRepository.DeleteByUserIdAndId(userId, id);
        if (foundRatedEntry == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
