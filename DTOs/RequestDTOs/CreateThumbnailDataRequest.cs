using System.ComponentModel.DataAnnotations;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class CreateThumbnailDataRequest
{
    public bool UseAutoGenerate { get; set; }

    [StringLength(128)]
    public string? AutoGenerateKeyword { get; set; }
}
