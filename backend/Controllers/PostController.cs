using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Imagegram.Repositories;
using Imagegram.Services;
using Imagegram.Models.Entity;
using Imagegram.Models.API;
using Imagegram.Exceptions.File;
using System.Security.Claims;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly PostService postService = default!;
    private readonly IWebHostEnvironment environment = default!;

    public PostController(
        ILogger<UserController> logger,
        IWebHostEnvironment environment,
        DbContextOptions<PostgresContext> options
    )
    {
        this.logger = logger;
        this.environment = environment;
        postService = new PostService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingPost>>> GetAllPostAsync()
    {
        var posts =
            (await postService.GetAllPosts()).Select(post => PostMapper.FromModel(post)).ToList()
            ?? new List<ExistingPost>();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostAsync(int id)
    {
        Post? post = await postService.GetPostById(id);
        return Ok(post);
    }

    [HttpPost("")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<ExistingPost>> CreatePostAsync([FromForm] NewPost post)
    {
        Claim userIdClaim = User.Claims
            .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
            .First();
        int userId = int.Parse(userIdClaim.Value);
        post.CreatorId = userId;
        var imageStorageFolder = Path.Combine(environment.WebRootPath, "images");
        try
        {
            var images = PostMapper.ImageCollectionFromFormFileCollection(
                post.Images,
                imageStorageFolder
            );
            Post newPost = await postService.CreateNewPost(PostMapper.ToModel(post, images));
            return Ok(PostMapper.FromModel(newPost));
        }
        catch (ImageFileFormatException)
        {
            return new UnsupportedMediaTypeResult();
        }
    }
}
