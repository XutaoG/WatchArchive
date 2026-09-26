using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("select_options")]
public class SelectOption
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [ForeignKey(nameof(AttributeDefinition))]
    [Column("attribute_definition_id")]
    public Guid AttributeDefinitionId { get; set; }

    public required AttributeDefinition AttributeDefinition { get; set; }

    [Column("value")]
    [MaxLength(32)]
    public required string Value { get; set; }
}
