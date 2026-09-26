using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

public abstract class BaseAttribute
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [ForeignKey(nameof(RatedEntry))]
    [Column("rated_entry_id")]
    public Guid RatedEntryId { get; set; }

    public required RatedEntry RatedEntry { get; set; }

    [ForeignKey(nameof(AttributeDefinition))]
    [Column("attribute_definition_id")]
    public Guid AttributeDefinitionId { get; set; }

    public required AttributeDefinition AttributeDefinition { get; set; }
}
