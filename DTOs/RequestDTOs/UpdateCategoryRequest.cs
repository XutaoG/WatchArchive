using System.ComponentModel.DataAnnotations;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class UpdateCategoryRequest
{
    [Required]
    [MaxLength(32)]
    public required string Name { get; set; }
}
