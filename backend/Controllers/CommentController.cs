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
    private readonly ILogger<UserController> _logger;
    private readonly CommentService commentService = default!;

    public CommentController(
        ILogger<UserController> logger,
        IWebHostEnvironment environment,
        DbContextOptions<PostgresContext> options
    )
    {
        _logger = logger;
        commentService = new CommentService(options);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExistingComment>>> GetAllCommentAsync()
    {
        var Comments =
            (await commentService.GetAllCommentsAsync())
                .Select(comment => new ExistingComment(comment))
                .ToList() ?? new List<ExistingComment>();
        return Ok(Comments);
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
        Claim UserIdClaim = User.Claims
            .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
            .First();
        int UserId = int.Parse(UserIdClaim.Value);
        comment.CreatorId = UserId;
        var NewComment = await commentService.CreateNewCommentAsync(CommentMapper.ToModel(comment));
        return Ok(CommentMapper.FromModel(NewComment));
    }
}
