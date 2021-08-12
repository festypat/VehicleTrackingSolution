using Microsoft.EntityFrameworkCore;

namespace VehicleTracking.Vehicle.Persistance.Context
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
         : base(options)
        {
        }
        public DbSet<Domain.Entities.Vehicle> Vehicle { get; set; }
    }
}
