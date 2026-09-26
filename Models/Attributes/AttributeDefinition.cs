using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchArchive.Server.Enums;

namespace WatchArchive.Server.Models.Attributes;

[Table("attribute_definitions")]
public class AttributeDefinition
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(32)]
    public required string Name { get; set; }

    [Column("type", TypeName = "smallint")]
    public required AttributeType Type { get; set; }

    [Column("user_id")]
    public required Guid UserId { get; set; }

    public required User User { get; set; }
}
