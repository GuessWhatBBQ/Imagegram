namespace Imagegram.Exceptions.Image;

[Serializable]
public class ImageNotFoundException : Exception
{
    public ImageNotFoundException()
        : base() { }

    public ImageNotFoundException(string message)
        : base(message) { }

    public ImageNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
