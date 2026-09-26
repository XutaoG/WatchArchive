using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("number_attributes")]
public class NumberAttribute : BaseAttribute
{
    [Column("value")]
    public decimal Value { get; set; }
}
