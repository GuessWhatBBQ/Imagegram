namespace Imagegram.Exceptions.File;

[Serializable]
public class ImageFileFormatException : Exception
{
    public ImageFileFormatException()
        : base() { }

    public ImageFileFormatException(string message)
        : base(message) { }

    public ImageFileFormatException(string message, Exception inner)
        : base(message, inner) { }
}
