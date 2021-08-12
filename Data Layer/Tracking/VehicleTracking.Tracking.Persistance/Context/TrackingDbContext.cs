using Microsoft.EntityFrameworkCore;
using VehicleTracking.Tracking.Domain.Entities;

namespace VehicleTracking.Tracking.Persistance.Context
{
    public class TrackingDbContext : DbContext
    {
        public TrackingDbContext(DbContextOptions<TrackingDbContext> options)
        : base(options)
        {
        }
        public DbSet<Location> Location { get; set; }
        public DbSet<TrackingHistory> TrackingHistory { get; set; }
    }
}
