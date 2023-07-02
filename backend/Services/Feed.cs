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

    public async Task<IEnumerable<Post>> GetFeedAfterTime(DateTime after)
    {
        return await db.Posts.Where(post => post.CreationDate >= after).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPaginatedFeedAfterTime(
        DateTime after,
        int skip,
        int size
    )
    {
        return (await GetFeedAfterTime(after)).Skip(skip).Take(size);
    }
}
