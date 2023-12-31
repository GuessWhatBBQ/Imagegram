namespace Imagegram.Repositories;

using Microsoft.EntityFrameworkCore;

using Imagegram.Models.Entity;

public class PostgresContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<Image> Images { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options) { }
}
