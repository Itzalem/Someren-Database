using Microsoft.EntityFrameworkCore;
using Someren_Database.Models;

namespace Someren_Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Activity> Activity { get; set; }
    }
}
