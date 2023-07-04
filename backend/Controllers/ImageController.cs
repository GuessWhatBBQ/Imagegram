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
    private readonly ILogger<ImageController> _logger;
    private readonly ImageService ImageService;

    public ImageController(
        ILogger<ImageController> logger,
        DbContextOptions<PostgresContext> options
    )
    {
        _logger = logger;
        ImageService = new ImageService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Image>>> GetAllImagesAsync()
    {
        var Images = (await ImageService.GetAllImages()).ToList();
        return Images;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Image>> GetImageById(int id)
    {
        try
        {
            Image Image = await ImageService.GetImageById(id);
            return Ok(Image);
        }
        catch (ImageNotFoundException)
        {
            return NotFound();
        }
    }
}
