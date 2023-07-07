using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Imagegram.Repositories;
using Imagegram.Services;
using Imagegram.Models.Entity;
using Imagegram.Models.API;
using System.Security.Claims;

namespace Imagegram.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly CommentService commentService = default!;

    public CommentController(
        ILogger<UserController> logger,
        IWebHostEnvironment environment,
        DbContextOptions<PostgresContext> options
    )
    {
        this.logger = logger;
        commentService = new CommentService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingComment>>> GetAllCommentAsync()
    {
        var comments =
            (await commentService.GetAllCommentsAsync())
                .Select(comment => new ExistingComment(comment))
                .ToList() ?? new List<ExistingComment>();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExistingComment>> GetCommentAsync(int id)
    {
        var comment = await commentService.GetCommentByIdAsync(id);
        return Ok(CommentMapper.FromModel(comment));
    }

    [HttpPost("")]
    [Authorize(AuthenticationSchemes = nameof(SessionHeaderAuthHandler))]
    public async Task<ActionResult<ExistingComment>> CreateCommentAsync(NewComment comment)
    {
        Claim userIdClaim = User.Claims
            .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
            .First();
        int userId = int.Parse(userIdClaim.Value);
        comment.CreatorId = userId;
        var newComment = await commentService.CreateNewCommentAsync(CommentMapper.ToModel(comment));
        return Ok(CommentMapper.FromModel(newComment));
    }
}
