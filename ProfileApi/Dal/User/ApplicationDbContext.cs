using Dal.User.Model;
using Microsoft.EntityFrameworkCore;

namespace Dal.User;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}

    public DbSet<UserDal> Users { get; set; }
}