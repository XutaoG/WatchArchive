using System.ComponentModel.DataAnnotations;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class CreateCategoryRequest
{
    [Required]
    [MaxLength(32)]
    public required string Name { get; set; }
}
