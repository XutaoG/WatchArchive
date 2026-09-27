using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WatchArchive.Server.Models;

[Table("tag_entries")]
[PrimaryKey(nameof(TagId), nameof(RatedEntryId))]
public class TagEntry
{
    [ForeignKey(nameof(Tag))]
    [Column("tag_id")]
    public Guid TagId { get; set; }

    public Tag Tag { get; set; } = null!;

    [ForeignKey(nameof(RatedEntry))]
    [Column("rated_entry_id")]
    public Guid RatedEntryId { get; set; }

    public RatedEntry RatedEntry { get; set; } = null!;
}
