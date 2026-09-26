using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models;

[Table("thumbnails")]
public class Thumbnail
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("img_loc")]
    [MaxLength(256)]
    public required string ImgLoc { get; set; }

    [Column("use_auto_generate")]
    public bool UseAutoGenerate { get; set; }

    [Column("auto_generate_keyword")]
    [MaxLength(128)]
    public string? AutoGenerateKeyword { get; set; }
}
