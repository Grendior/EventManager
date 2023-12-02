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
    }
}
