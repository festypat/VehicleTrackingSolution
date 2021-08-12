namespace VehicleTracking.Tracking.Helper.Dto.Request
{
    public class CreateLocationDto
    {
        public long VehicleId { get; set; }
        public long DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
