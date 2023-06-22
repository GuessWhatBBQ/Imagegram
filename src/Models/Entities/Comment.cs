using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagegram.Models.Entity;

[Table("comment")]
public class Comment
{
    [Key]
    [Column("id")]
    public int CommentId { get; set; }

    [Column("postid")]
    public int PostId { get; set; }

    [Column("creatorid")]
    public int CreatorId { get; set; }

    [Column("content")]
    public string Content { get; set; } = default!;

    [Column("creationdate")]
    public DateTime CreationDate { get; set; }

    public void Deconstruct(out int commentId, out int postId, out int creatorId, out string content, out DateTime creationDate)
    {
        commentId = CommentId;
        postId = PostId;
        creatorId = CreatorId;
        content = Content;
        creationDate = CreationDate;
    }
}
