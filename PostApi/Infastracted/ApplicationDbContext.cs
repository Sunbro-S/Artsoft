using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infastracted;

public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<LikeCounter> LikeCounters { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}