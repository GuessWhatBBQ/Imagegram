namespace Imagegram.Services;

using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Microsoft.EntityFrameworkCore;

public class FeedService
{
    private readonly PostgresContext db = default!;

    public FeedService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public async Task<IEnumerable<Post>> GetFeedAfterTime(DateTime after, int lastId)
    {
        return await db.Posts
            .Include(post => post.Images)
            .Include(post => post.Comments.OrderByDescending(post => post.CreationDate).Take(2))
            .OrderBy(b => b.CreationDate)
            .ThenBy(b => b.PostId)
            .Where(b => b.CreationDate > after || (b.CreationDate == after && b.PostId > lastId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPaginatedFeedAfterTime(
        DateTime after,
        int size,
        int skip = 0
    )
    {
        var posts = (await GetFeedAfterTime(after, skip)).Take(size);
        return posts;
    }
}
