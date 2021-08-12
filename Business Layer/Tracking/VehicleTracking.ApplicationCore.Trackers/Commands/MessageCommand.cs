namespace VehicleTracking.ApplicationCore.Trackers.Commands
{
    public class MessageCommand : Command
    {
        public long VehicleId { get; set; }
        public long DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
