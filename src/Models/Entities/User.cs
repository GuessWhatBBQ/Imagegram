using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagegram.Models.Entity;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int UserId { get; set; }

    [Column("username")]
    public string UserName { get; set; } = default!;

    [Column("fullname")]
    public string FullName { get; set; } = default!;

    [Column("password")]
    public string Password { get; set; } = default!;

    [ForeignKey("UserId")]
    public ICollection<Session> Sessions { get; set; } = default!;

    [ForeignKey("CreatorId")]
    public ICollection<Post> Posts { get; set; } = default!;

    [ForeignKey("CreatorId")]
    public ICollection<Comment> Comments { get; set; } = default!;

    public void Deconstruct(
        out int userId,
        out string userName,
        out string fullName,
        out string password
    )
    {
        userId = UserId;
        userName = UserName;
        fullName = FullName;
        password = Password;
    }
}
