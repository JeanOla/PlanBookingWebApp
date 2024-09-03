using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.data
{
    public class ClassDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PlaneBookingDB;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ModelMap();
            base.OnModelCreating(builder);
        }
        public DbSet<Booking> booking { get; set; }
        public DbSet<Flight> flight { get; set; }
        public DbSet<Plane> plane { get; set; }
        public DbSet<Airport>airport { get; set; }
    }
}
