using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("url_attributes")]
public class UrlAttribute : BaseAttribute
{
    [Column("value")]
    [MaxLength(2048)]
    public required string Value { get; set; }
}
