namespace Imagegram.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Models.Entity;
using Imagegram.Services;
using Imagegram.Exceptions.Image;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> logger;
    private readonly ImageService imageService;

    public ImageController(
        ILogger<ImageController> logger,
        DbContextOptions<PostgresContext> options
    )
    {
        this.logger = logger;
        imageService = new ImageService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Image>>> GetAllImagesAsync()
    {
        var images = (await imageService.GetAllImages()).ToList();
        return images;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetImageById(int id)
    {
        try
        {
            Image image = await imageService.GetImageById(id);
            return Ok(image);
        }
        catch (ImageNotFoundException)
        {
            return NotFound();
        }
    }
}
