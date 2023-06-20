using Imagegram.Models.Entity;

namespace Imagegram.Models.API;

public class NewUser
{
    public string UserName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;

    public void Deconstruct(out string userName, out string fullName)
    {
        userName = UserName;
        fullName = FullName;
    }

    public void Deconstruct(out string userName, out string fullName, out string password)
    {
        userName = UserName;
        fullName = FullName;
        password = Password;
    }
}

public class ExistingUser : NewUser
{
    public int UserId { get; set; }

    public ExistingUser() { }
    public ExistingUser(User user)
    {
        (UserId, UserName, FullName) = user;
    }

    public void Deconstruct(out int userId, out string userName, out string fullName)
    {
        userId = UserId;
        userName = UserName;
        fullName = FullName;
    }

}

public class UserMapper
{

    public static User ToModel(NewUser user)
    {
        var (UserName, FullName) = user;
        return new User { UserName = UserName, FullName = FullName };
    }

    public static User ToModel(ExistingUser user)
    {
        var (UserId, UserName, FullName) = user;
        return new User { UserName = UserName, UserId = UserId, FullName = FullName };
    }

    public static ExistingUser ResponseFromModel(User user)
    {
        var (UserId, UserName, FullName) = user;
        return new ExistingUser { UserName = UserName, UserId = UserId, FullName = FullName };
    }
}
