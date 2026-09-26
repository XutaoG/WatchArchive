using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("long_text_attributes")]
public class LongTextAttribute : BaseAttribute
{
    [Column("value")]
    [MaxLength(65536)]
    public required string Value { get; set; }
}
