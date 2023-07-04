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
public class FeedController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly FeedService FeedService = default!;

    public FeedController(
        ILogger<UserController> logger,
        IWebHostEnvironment environment,
        DbContextOptions<PostgresContext> options
    )
    {
        _logger = logger;
        FeedService = new FeedService(options);
    }

    public class FeedQueryParams
    {
        public DateTime After { get; set; }
        public int Size { get; set; }
        public int Skip { get; set; }

        public void Deconstruct(out DateTime after, out int skip, out int size)
        {
            after = After;
            skip = Skip;
            size = Size;
        }
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<ExistingPost>>> GetAllPostAsync(
        [FromQuery] FeedQueryParams queryParams
    )
    {
        var (after, skip, size) = queryParams;
        var Posts =
            (await FeedService.GetPaginatedFeedAfterTime(after, skip, size)).Select(
                post => PostMapper.FromModel(post)
            ) ?? new List<ExistingPost>();
        return Ok(Posts);
    }
}
