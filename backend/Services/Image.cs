namespace Imagegram.Services;

using Imagegram.Exceptions.Image;
using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Microsoft.EntityFrameworkCore;

public class ImageService
{
    private readonly PostgresContext db = default!;

    public ImageService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public ImageService() { }

    public async Task<IEnumerable<Image>> GetAllImages()
    {
        return await db.Images.ToListAsync();
    }

    public async Task<Image> GetImageById(int id)
    {
        return await db.Images.FindAsync(id) ?? throw new ImageNotFoundException();
    }

    public async Task<Image> CreateNewPost(Image image)
    {
        var newImage = await db.AddAsync(image);
        await db.SaveChangesAsync();
        return newImage.Entity;
    }

    public async Task<Image> UpdatePost(Image image)
    {
        var updatedImage = await db.Images.FindAsync(image.ImageId) ?? throw new ImageNotFoundException();
        updatedImage.ImagePath = image.ImagePath;
        await db.SaveChangesAsync();
        return updatedImage;
    }

    public async Task<Image> DeletePost(Image image)
    {
        var deletedImage = await GetImageById(image.ImageId);
        db.Images.Remove(deletedImage);
        await db.SaveChangesAsync();
        return deletedImage;
    }
}
