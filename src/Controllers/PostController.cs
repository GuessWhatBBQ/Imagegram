using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Imagegram.Repositories;
using Imagegram.Services;
using Imagegram.Models.Entity;
using Imagegram.Models.API;
using Imagegram.Exceptions.User;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly PostgresContext db = default!;
    private readonly PostService PostService = default!;

    public PostController(ILogger<UserController> logger, DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
        _logger = logger;
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
    public async Task<ActionResult<Post>> CreatePostAsync([FromForm] NewPost post)
    {
        Post NewPost = PostMapper.ToModel(post);
        await PostService.CreateNewPost(NewPost);
        return NewPost;
    }
}
