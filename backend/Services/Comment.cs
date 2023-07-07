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
        comment.CreationDate = DateTime.UtcNow;
        var newComment = await db.AddAsync(comment);
        await db.SaveChangesAsync();
        return newComment.Entity;
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        var updatedComment =
            await db.Comments.FindAsync(comment.CommentId)
            ?? new Comment { CreatorId = comment.CreatorId };
        updatedComment.Content = comment.Content;
        await db.SaveChangesAsync();
        return updatedComment;
    }

    public async Task<Comment> DeleteCommentAsync(Comment comment)
    {
        var deletedComment = await GetCommentByIdAsync(comment.CommentId);
        db.Comments.Remove(deletedComment);
        await db.SaveChangesAsync();
        return deletedComment;
    }
}
