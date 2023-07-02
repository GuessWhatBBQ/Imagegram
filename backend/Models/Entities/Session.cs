using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagegram.Models.Entity;

[Table("sessions")]
public class Session
{
    [Key]
    [Column("id")]
    public int SessionId { get; set; }

    [Column("userid")]
    public int UserId { get; set; }

    [Column("sessiontoken")]
    public string SessionToken { get; set; } = default!;

    // [ForeignKey("UserId")]
    // public User User = default!;

    public void Deconstruct(out int sessionId, out int userId, out string sessionToken)
    {
        sessionId = SessionId;
        userId = UserId;
        sessionToken = SessionToken;
    }

    public override string ToString()
    {
        return $"SessionToken = {SessionToken}";
    }
}
