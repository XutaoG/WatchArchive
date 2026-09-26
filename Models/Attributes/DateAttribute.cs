using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("date_attributes")]
public class DateAttribute : BaseAttribute
{
    [Column("value")]
    public DateTime Value { get; set; }
}
