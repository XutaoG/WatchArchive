using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models.Attributes;

[Table("single_select_attributes")]
public class SingleSelectAttribute : BaseAttribute
{
    [ForeignKey(nameof(SelectOption))]
    [Column("select_option_id")]
    public Guid? SelectOptionId { get; set; }

    public SelectOption? SelectOption { get; set; }
}
