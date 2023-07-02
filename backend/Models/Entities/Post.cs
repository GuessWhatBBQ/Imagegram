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
    [ForeignKey("User")]
    public int CreatorId { get; set; }

    [Column("caption")]
    public string Caption { get; set; } = default!;

    [Column("creationdate")]
    public DateTime CreationDate { get; set; }

    [ForeignKey("PostId")]
    public ICollection<Image> Images { get; set; } = default!;

    [ForeignKey("PostId")]
    public ICollection<Comment> Comments { get; set; } = default!;

    public void Deconstruct(out int postId, out int creatorId, out string caption)
    {
        postId = PostId;
        creatorId = CreatorId;
        caption = Caption;
    }
}
