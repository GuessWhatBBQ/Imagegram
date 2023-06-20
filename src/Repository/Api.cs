namespace Imagegram.Repositories;

using Microsoft.EntityFrameworkCore;

using Imagegram.Models.Entity;

public class ApiContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;

    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");
}

