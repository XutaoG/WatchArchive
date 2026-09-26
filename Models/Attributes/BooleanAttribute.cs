using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("boolean_attributes")]
public class BooleanAttribute : BaseAttribute
{
    [Column("value")]
    public bool Value { get; set; }
}
