using Microsoft.EntityFrameworkCore;
using Parkomatic.Models;

namespace Parkomatic.Data
{
    public class ParkomaticContext : DbContext
    {
        public ParkomaticContext(DbContextOptions<ParkomaticContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Pass> Passes { get; set; }

        public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}
