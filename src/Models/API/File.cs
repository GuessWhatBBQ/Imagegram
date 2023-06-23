namespace Imagegram.Models.API;

using Imagegram.Exceptions.File;
using Imagegram.Helpers.File;

public static class MimeTypes
{
    public static readonly IEnumerable<string> AcceptableImageTypes = new List<string> { "image/jpeg", "image/png", "image/bmp" };
    public static readonly IEnumerable<string> StorageImageTypes = new List<string> { "image/jpeg" };
}

public class FileMapper
{
    public static string StoreFormFileAsync(string localFolder, IFormFile file)
    {
        if (!isAcceptableImageType(file.ContentType))
        {
            throw new ImageFileFormatException();
        }
        var uniqueFileName = FileHelper.GetUniqueFileName(file.FileName);
        var filePath = Path.Combine(localFolder, uniqueFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new IOException());
        if (MimeTypes.StorageImageTypes.Contains(file.ContentType))
        {
            StoreImageFileAsync(filePath, file);
        }
        else
        {
            ConvertAndStoreImageFileAsync(filePath, file);
        }
        return filePath;
    }

    public static bool isAcceptableImageType(string mimeType)
    {
        return MimeTypes.AcceptableImageTypes.Contains(mimeType);
    }

    private async static void StoreImageFileAsync(string filePath, IFormFile file)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
    }

    private async static void ConvertAndStoreImageFileAsync(string filePath, IFormFile file)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            Image image = await Image.LoadAsync(memoryStream);
            image.SaveAsJpeg(filePath);
        }
        catch
        {
            Console.WriteLine("Could not convert image");
        }
    }
}
