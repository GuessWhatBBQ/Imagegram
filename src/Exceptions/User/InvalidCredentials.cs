namespace Imagegram.Exceptions.User;

[Serializable]
public class InvalidUserCredentialsException : Exception
{
    public InvalidUserCredentialsException() : base() { }
    public InvalidUserCredentialsException(string message) : base(message) { }
    public InvalidUserCredentialsException(string message, Exception inner) : base(message, inner) { }
}
