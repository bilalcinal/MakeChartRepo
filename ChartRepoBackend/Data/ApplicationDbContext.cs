using Microsoft.EntityFrameworkCore;

namespace ChartRepoBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDatabase> UserDatabases { get; set; }
    }
}
