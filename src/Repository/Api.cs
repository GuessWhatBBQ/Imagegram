namespace Imagegram.Repositories;

using Microsoft.EntityFrameworkCore;

using Imagegram.Models.Entity;

public class PostgresContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<Image> Image { get; set; } = default!;

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options) { }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");
}
