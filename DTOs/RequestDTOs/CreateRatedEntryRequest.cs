using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class CreateRatedEntryRequest
{
    [Required]
    [FromForm]
    public required string EntryData { get; set; }

    [Required]
    [FromForm]
    public required string ThumbnailData { get; set; }

    [FromForm]
    public IFormFile? Thumbnail { get; set; }
}
