namespace Imagegram.Models.API;

public class AuthCredentials
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;

    public void Deconstruct(out string username, out string password)
    {
        username = UserName;
        password = Password;
    }
}
