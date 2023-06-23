namespace Imagegram.Services;

using Imagegram.Exceptions.Post;
using Imagegram.Models.Entity;
using Imagegram.Repositories;
using Microsoft.EntityFrameworkCore;

public class CommentService
{
    private readonly PostgresContext db = default!;

    public CommentService(DbContextOptions<PostgresContext> options)
    {
        db = new PostgresContext(options);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        return await db.Comments.ToListAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await db.Comments.Where(comment => comment.CommentId == id).FirstOrDefaultAsync()
            ?? throw new PostNotFoundException();
    }

    public async Task<Comment> CreateNewCommentAsync(Comment comment)
    {
        var NewComment = await db.AddAsync(comment);
        await db.SaveChangesAsync();
        return NewComment.Entity;
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        var Comment =
            await db.Comments.FindAsync(comment.CommentId)
            ?? new Comment { CreatorId = comment.CreatorId };
        Comment.Content = comment.Content;
        await db.SaveChangesAsync();
        return Comment;
    }

    public async Task<Comment> DeleteCommentAsync(Comment post)
    {
        var Comment = await GetCommentByIdAsync(post.CommentId);
        db.Comments.Remove(Comment);
        await db.SaveChangesAsync();
        return Comment;
    }
}
