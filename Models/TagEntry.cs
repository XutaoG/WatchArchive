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

    public required Tag Tag { get; set; }

    [ForeignKey(nameof(RatedEntry))]
    [Column("rated_entry_id")]
    public Guid RatedEntryId { get; set; }

    public required RatedEntry RatedEntry { get; set; }
}
