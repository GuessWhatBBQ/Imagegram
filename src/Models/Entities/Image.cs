using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagegram.Models.Entity;

[Table("image")]
public class Image
{
    [Key]
    [Column("id")]
    public int ImageId { get; set; }

    [Column("postid")]
    public int PostId { get; set; }

    [Column("imagepath")]
    public string ImagePath { get; set; } = default!;

    // [ForeignKey("PostId")]
    // public Post Post { get; set; } = default!;

    public void Deconstruct(out int imageId, out int postId, out string imagePath)
    {
        imageId = ImageId;
        postId = PostId;
        imagePath = ImagePath;
    }
}
