using Microsoft.EntityFrameworkCore;
using UsersApp.Data.EntityModels;

namespace UsersApp.Data
{
    public class SandBoxDbContext : DbContext
    {
        public SandBoxDbContext(DbContextOptions<SandBoxDbContext> options)
            : base(options)
        {
        }

        public DbSet<eUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
