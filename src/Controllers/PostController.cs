using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Imagegram.Repositories;
using Imagegram.Services;
using Imagegram.Models.Entity;
using Imagegram.Models.API;
using Imagegram.Exceptions.File;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly PostService PostService = default!;
    private readonly IWebHostEnvironment Environment = default!;

    public PostController(
        ILogger<UserController> logger,
        IWebHostEnvironment environment,
        DbContextOptions<PostgresContext> options
    )
    {
        _logger = logger;
        Environment = environment;
        PostService = new PostService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingPost>>> GetAllPostAsync()
    {
        var Posts =
            (await PostService.GetAllPosts()).Select(post => new ExistingPost(post)).ToList()
            ?? new List<ExistingPost>();
        return Ok(Posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostAsync(int id)
    {
        Post? Post = await PostService.GetPostById(id);
        return Ok(Post);
    }

    [HttpPost("")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<ExistingPost>> CreatePostAsync([FromForm] NewPost post)
    {
        var imageStorageFolder = Path.Combine(Environment.WebRootPath, "images");
        try
        {
            var images = PostMapper.ImageCollectionFromFormFileCollection(
                post.Images,
                imageStorageFolder
            );
            Post NewPost = await PostService.CreateNewPost(PostMapper.ToModel(post, images));
            return Ok(PostMapper.FromModel(NewPost));
        }
        catch (ImageFileFormatException)
        {
            return new UnsupportedMediaTypeResult();
        }
    }
}
