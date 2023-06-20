namespace Imagegram.Exceptions.Session;

[Serializable]
public class SessionNotFoundException : Exception
{
    public SessionNotFoundException() : base() { }
    public SessionNotFoundException(string message) : base(message) { }
    public SessionNotFoundException(string message, Exception inner) : base(message, inner) { }
}
