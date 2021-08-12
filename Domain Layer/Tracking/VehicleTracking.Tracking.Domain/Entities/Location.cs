using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTracking.Tracking.Domain.Entities
{
    public class Location : BaseEntity
    {
        public long LocationId { get; set; }
        public long VehicleId { get; set; }
        public long DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string PlaceId { get; set; }
        [Column(TypeName = "NVARCHAR(60)")]
        public string Licence { get; set; }
        [Column(TypeName = "NVARCHAR(350)")]
        public string DisplayName { get; set; }
        public DateTimeOffset DateEntered { get; set; } = DateTime.Now;
        public DateTimeOffset LastDateModified { get; set; }
    }
}
