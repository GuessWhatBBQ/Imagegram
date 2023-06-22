namespace Imagegram.Models.API;

using Imagegram.Helpers.File;

public class FileMapper
{
    public static async Task<string> StoreFormFileAsync(string localFolder, IFormFile file)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(file.FileName);
        var filePath = Path.Combine(localFolder, uniqueFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new IOException());
        await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        return filePath;
    }
}

