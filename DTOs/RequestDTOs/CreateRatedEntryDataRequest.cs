using System.ComponentModel.DataAnnotations;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class CreateRatedEntryDataRequest
{
    [Required]
    [StringLength(128)]
    public required string Name { get; set; }

    [Range(0, 5)]
    public decimal OverallRating { get; set; }

    public List<Guid> CategoryIds { get; set; } = [];

    public List<Guid> TagIds { get; set; } = [];
}
