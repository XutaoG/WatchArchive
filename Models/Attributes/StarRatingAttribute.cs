using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("star_rating_attributes")]
public class StarRatingAttribute : BaseAttribute
{
    [Column("value", TypeName = "decimal(3, 1)")]
    public decimal Value { get; set; }
}
