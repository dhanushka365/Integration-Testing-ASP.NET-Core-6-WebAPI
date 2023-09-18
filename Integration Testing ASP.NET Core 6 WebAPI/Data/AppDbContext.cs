using Microsoft.EntityFrameworkCore;

namespace Integration_Testing_ASP.NET_Core_6_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .Property(p => p.Id)
                .UseIdentityColumn();
        }

    }
}
