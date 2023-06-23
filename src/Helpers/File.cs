namespace Imagegram.Helpers.File;

public class FileHelper
{
    public static string GetUniqueFileName(string fileName)
    {
        return string.Concat(
            Path.GetFileNameWithoutExtension(fileName),
            "_",
            Guid.NewGuid().ToString(),
            Path.GetExtension(fileName)
        );
    }
}
