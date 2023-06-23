namespace Imagegram.Services;

using Imagegram.Exceptions.Post;
using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Microsoft.EntityFrameworkCore;

public class PostService
{
    private readonly PostgresContext db = default!;

    public PostService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        return await db.Posts.ToListAsync();
    }

    public async Task<Post> GetPostById(int id)
    {
        return await db.Posts
                .Include(post => post.Images)
                .Include(post => post.Comments)
                .Where(post => post.PostId == id)
                .FirstOrDefaultAsync() ?? throw new PostNotFoundException();
    }

    public async Task<Post> CreateNewPost(Post post)
    {
        post.CreationDate = DateTime.UtcNow;
        var NewPost = await db.AddAsync(post);
        await db.SaveChangesAsync();
        return NewPost.Entity;
    }

    public async Task<Post> UpdatePost(Post post)
    {
        var Post = await db.Posts.FindAsync(post.PostId) ?? new Post { CreatorId = post.CreatorId };
        Post.Caption = post.Caption;
        await db.SaveChangesAsync();
        return Post;
    }

    public async Task<Post> DeletePost(Post post)
    {
        var Post = await GetPostById(post.PostId);
        db.Posts.Remove(Post);
        await db.SaveChangesAsync();
        return Post;
    }
}
