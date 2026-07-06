using EventReservationMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EventReservationMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}