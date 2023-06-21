namespace Imagegram.Exceptions.Post;

[Serializable]
public class PostNotFoundException : Exception
{
    public PostNotFoundException()
        : base() { }

    public PostNotFoundException(string message)
        : base(message) { }

    public PostNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
