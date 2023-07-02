namespace Imagegram.Exceptions.User;

[Serializable]
public class UserNotFoundException : Exception
{
    public UserNotFoundException()
        : base() { }

    public UserNotFoundException(string message)
        : base(message) { }

    public UserNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
