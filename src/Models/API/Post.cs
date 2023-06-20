using Imagegram.Models.Entity;

namespace Imagegram.Models.API;

public class NewPost
{
    public int CreatorId { get; set; } = default!;
    public string Caption { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;


    public void Deconstruct(out int creatorId, out string caption)
    {
        creatorId = CreatorId;
        caption = Caption;
    }
}

public class ExistingPost : NewPost
{
    public int PostId { get; set; }

    public ExistingPost() { }
    public ExistingPost(Post post)
    {
        (PostId, CreatorId, Caption) = post;
    }

    public void Deconstruct(out int postId, out int creatorId, out string caption)
    {
        postId = PostId;
        creatorId = CreatorId;
        caption = Caption;
    }

}

public class PostMapper
{
    public static Post ToModel(NewPost post)
    {
        var (CreatorId, Caption) = post;
        return new Post { CreatorId = CreatorId, Caption = Caption };
    }

    public static Post ToModel(ExistingPost post)
    {
        var (PostId, CreatorId, Caption) = post;
        return new Post { PostId = PostId, CreatorId = CreatorId, Caption = Caption };
    }

    public static ExistingPost FromModel(Post post)
    {
        var (PostId, CreatorId, Caption) = post;
        return new ExistingPost { PostId = PostId, CreatorId = CreatorId, Caption = Caption };
    }
}
