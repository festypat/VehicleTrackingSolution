using System;

namespace VehicleTracking.Vehicle.Helper.ViewModel
{
    public class VehicleViewModel
    {
        public long VehicleId { get; set; }
        public string LicensePlateNumber { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public string VehicleName { get; set; }
        public string Maker { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateRegistered { get; set; } 
        public DateTimeOffset LastDateModified { get; set; }
    }
}
