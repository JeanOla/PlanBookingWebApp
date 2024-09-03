using Microsoft.EntityFrameworkCore;
using PlanBookingWebApp.Models;

namespace PlanBookingWebApp.data
{
    public static class AppModelMapping
    {
         public static void ModelMap(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Flight>()
               .HasOne(a => a.airport)
               .WithMany(f => f.flight)
               .HasForeignKey(fk => fk.AirportId);

            modelBuilder.Entity<Flight>()
              .HasOne(p => p.plane)
              .WithMany(f => f.flight)
              .HasForeignKey(fk => fk.PlaneId);


            modelBuilder.Entity<Booking>()
               .HasOne(f => f.Flight)
               .WithMany(b => b.booking)
               .HasForeignKey(fk => fk.FlightId);
        }
    }
}
