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
        var NewImage = await db.AddAsync(image);
        await db.SaveChangesAsync();
        return NewImage.Entity;
    }

    public async Task<Image> UpdatePost(Image image)
    {
        var Image = await db.Images.FindAsync(image.ImageId) ?? throw new ImageNotFoundException();
        Image.ImagePath = image.ImagePath;
        await db.SaveChangesAsync();
        return Image;
    }

    public async Task<Image> DeletePost(Image image)
    {
        var Image = await GetImageById(image.ImageId);
        db.Images.Remove(Image);
        await db.SaveChangesAsync();
        return Image;
    }
}
