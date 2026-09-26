using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("short_text_attributes")]
public class ShortTextAttribute : BaseAttribute
{
    [Column("value")]
    [MaxLength(32)]
    public required string Value { get; set; }
}
