using EventManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                new { Id = Guid.NewGuid(), Title = "Tytuł Wydarzenia", Date = DateTime.Now, StartTime = DateTime.Now, EndTime = DateTime.Now, Capacity = 30 },
                new { Id = Guid.NewGuid(), Title = "Tytuł Wydarzenia 2", Date = DateTime.Now, StartTime = DateTime.Now, EndTime = DateTime.Now, Capacity = 40 },
                new { Id = Guid.NewGuid(), Title = "Tytuł Wydarzenia 3", Date = DateTime.Now, StartTime = DateTime.Now, EndTime = DateTime.Now, Capacity = 50 }
            );
        }
    }
}
