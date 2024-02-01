using EventManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManager.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                new
                {
                    Id = Guid.NewGuid().ToString(), Title = "Zajęcia boks",
                    Description =
                        "<h1>Zajęcia Bokserskie w Słupsku</h1>\n<p>Witaj na stronie poświęconej naszym zajęciom bokserskim w Słupsku! Jesteśmy zespołem pasjonat&oacute;w, kt&oacute;rzy razem trenują i rozwijają swoje umiejętności bokserskie.</p>\n<h2>Jak Zacząć?</h2>\n<p>Jeśli jesteś zainteresowany/a dołączeniem do naszych zajęć, zapisz się do wydarzenia lub przyjdź na jedno z trening&oacute;w, aby dowiedzieć się więcej.</p>\n<p>Zapraszamy do wsp&oacute;lnego rozwijania umiejętności bokserskich i czerpania radości z trening&oacute;w!</p>",
                    Date = new DateTime(2024, 01, 30), StartTime = DateTime.Now, EndTime = DateTime.Now,
                    Capacity = 12, Occupied = 0,
                    ImageUrl = @"\images\event\050665e9-bb54-4c15-98fc-482b95af60c9.jpg"
                }
            );
        }
    }
}