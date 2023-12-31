namespace Imagegram.Models.API;

using Imagegram.Models.Entity;

public class NewPost
{
    public int CreatorId { get; set; } = default!;
    public string Caption { get; set; } = default!;
    public IFormFileCollection Images { get; set; } = default!;

    public void Deconstruct(out int creatorId, out string caption, out IFormFileCollection images)
    {
        creatorId = CreatorId;
        caption = Caption;
        images = Images;
    }
}

public class ExistingPost
{
    public int CreatorId { get; set; } = default!;
    public string Caption { get; set; } = default!;

    public class Image
    {
        public int Id { get; set; }
    }

    public class Comment
    {
        public string Content { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }
    }

    public IEnumerable<Image>? Images { get; set; } = default!;
    public IEnumerable<Comment>? Comments { get; set; } = default!;

    public int PostId { get; set; }

    public ExistingPost() { }

    public void Deconstruct(out int postId, out int creatorId, out string caption)
    {
        postId = PostId;
        creatorId = CreatorId;
        caption = Caption;
    }
}

public class PostMapper
{
    public static ICollection<Image> ImageCollectionFromFormFileCollection(
        IFormFileCollection files,
        string folderPath
    )
    {
        var images = new List<Image>();
        foreach (var Image in files)
        {
            var imagePath = FileMapper.StoreFormFileAsync(folderPath, Image);
            images.Add(new Image { ImagePath = imagePath });
        }
        return images;
    }

    public static Post ToModel(NewPost post, ICollection<Image> Images)
    {
        var (CreatorId, Caption, _) = post;
        return new Post
        {
            CreatorId = CreatorId,
            Caption = Caption,
            Images = Images
        };
    }

    public static Post ToModel(ExistingPost post)
    {
        var (PostId, CreatorId, Caption) = post;
        return new Post
        {
            PostId = PostId,
            CreatorId = CreatorId,
            Caption = Caption
        };
    }

    public static ExistingPost FromModel(Post post)
    {
        var (PostId, CreatorId, Caption, Images, Comments) = post;
        return new ExistingPost
        {
            PostId = PostId,
            CreatorId = CreatorId,
            Caption = Caption,
            Images = Images?.Select(image => FromModel(image)),
            Comments = Comments?.Select(comment => FromModel(comment)),
        };
    }

    public static ExistingPost.Image FromModel(Image image)
    {
        return new ExistingPost.Image() { Id = image.ImageId };
    }

    public static ExistingPost.Comment FromModel(Comment comment)
    {
        return new ExistingPost.Comment()
        {
            Content = comment.Content,
            CreationDate = comment.CreationDate,
            Id = comment.CommentId
        };
    }
}
