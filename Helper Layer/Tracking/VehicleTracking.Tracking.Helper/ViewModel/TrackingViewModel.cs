namespace VehicleTracking.Tracking.Helper.ViewModel
{
    public class TrackingViewModel
    {
        public long LocationId { get; set; }
        public long VehicleId { get; set; }
        public long DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PlaceId { get; set; }
        public string Licence { get; set; }
        public string DisplayName { get; set; }
    }
}
