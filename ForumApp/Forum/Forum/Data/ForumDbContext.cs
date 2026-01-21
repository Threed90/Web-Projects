using Forum.Data.Models;
using Forum.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
