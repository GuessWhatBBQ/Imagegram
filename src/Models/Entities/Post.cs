using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagegram.Models.Entity;

[Table("post")]
public class Post
{
    [Key]
    [Column("id")]
    public int PostId { get; set; }

    [Column("creatorid")]
    public int CreatorId { get; set; }

    [Column("caption")]
    public string Caption { get; set; } = default!;

    public ICollection<Image> Images { get; set; } = default!;

    public void Deconstruct(out int postId, out int creatorId, out string caption)
    {
        postId = PostId;
        creatorId = CreatorId;
        caption = Caption;
    }
}
