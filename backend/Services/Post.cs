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
        var newPost = await db.AddAsync(post);
        await db.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<Post> UpdatePost(Post post)
    {
        var updatedPost = await db.Posts.FindAsync(post.PostId) ?? new Post { CreatorId = post.CreatorId };
        updatedPost.Caption = post.Caption;
        await db.SaveChangesAsync();
        return updatedPost;
    }

    public async Task<Post> DeletePost(Post post)
    {
        var deletedPost = await GetPostById(post.PostId);
        db.Posts.Remove(deletedPost);
        await db.SaveChangesAsync();
        return deletedPost;
    }
}
