namespace Imagegram.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Models.Entity;
using Imagegram.Services;
using Imagegram.Exceptions.Image;

[ApiController]
[Route("[controller]")]
public class AssetController : ControllerBase
{
    private readonly ILogger<AssetController> logger;
    private readonly ImageService imageService;

    public AssetController(
        ILogger<AssetController> logger,
        DbContextOptions<PostgresContext> options
    )
    {
        this.logger = logger;
        imageService = new ImageService(options);
    }

    [HttpGet("image/{id}")]
    public async Task<ActionResult> GetImageById(int id)
    {
        try
        {
            Image Image = await imageService.GetImageById(id);
            var mimeType = "image/jpg";
            var fileStream = new FileStream(Image.ImagePath, FileMode.Open);
            return new FileStreamResult(fileStream, mimeType);
        }
        catch (ImageNotFoundException)
        {
            return NotFound();
        }
    }
}
