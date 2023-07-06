namespace Imagegram.Exceptions.User;

[Serializable]
public class UsernameAlreadyTaken : Exception
{
    public UsernameAlreadyTaken()
        : base() { }

    public UsernameAlreadyTaken(string message)
        : base(message) { }

    public UsernameAlreadyTaken(string message, Exception inner)
        : base(message, inner) { }
}
