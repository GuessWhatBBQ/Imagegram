using Imagegram.Models.Entity;

namespace Imagegram.Models.API;

public class NewComment
{
    public int PostId { get; set; } = default!;
    public int CreatorId { get; set; } = default!;
    public string Content { get; set; } = default!;

    public void Deconstruct(out int postId, out int creatorId, out string content)
    {
        postId = PostId;
        creatorId = CreatorId;
        content = Content;
    }
}

public class ExistingComment : NewComment
{
    public int CommentId { get; set; }
    public DateTime CreationDate { get; set; }

    public ExistingComment() { }

    public ExistingComment(Comment comment)
    {
        (CommentId, PostId, CreatorId, Content, CreationDate) = comment;
    }

    public void Deconstruct(
        out int commentId,
        out int postId,
        out int creatorId,
        out string content
    )
    {
        commentId = CommentId;
        postId = PostId;
        creatorId = CreatorId;
        content = Content;
    }
}

public class CommentMapper
{
    public static Comment ToModel(NewComment post)
    {
        var (postId, creatorId, content) = post;
        return new Comment
        {
            PostId = postId,
            CreatorId = creatorId,
            Content = content
        };
    }

    public static Comment ToModel(ExistingComment comment)
    {
        var (commentId, postId, creatorId, content) = comment;
        return new Comment
        {
            CommentId = commentId,
            PostId = postId,
            CreatorId = creatorId,
            Content = content
        };
    }

    public static ExistingComment FromModel(Comment comment)
    {
        var (commentId, postId, creatorId, content, creationDate) = comment;
        return new ExistingComment
        {
            CommentId = commentId,
            PostId = postId,
            CreatorId = creatorId,
            Content = content,
            CreationDate = creationDate,
        };
    }
}
