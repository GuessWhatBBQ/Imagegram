namespace Imagegram.Models.API;

using Imagegram.Exceptions.File;
using Imagegram.Helpers.File;

public static class MimeTypes
{
    public static readonly IEnumerable<string> AcceptableImageTypes = new List<string> { "image/jpeg", "image/png", "image/bmp" };
}

public class FileMapper
{
    public static async Task<string> StoreFormFileAsync(string localFolder, IFormFile file)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(file.FileName);
        var filePath = Path.Combine(localFolder, uniqueFileName);
        if (!isAcceptableImageType(file.ContentType))
        {
            throw new ImageFileFormatException();
        }
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new IOException());
        await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        return filePath;
    }

    public static bool isAcceptableImageType(string mimeType)
    {
        return MimeTypes.AcceptableImageTypes.Contains(mimeType);
    }
}
