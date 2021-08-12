namespace VehicleTracking.ApplicationCore.Trackers.Commands
{
    public class CreateNewLocationCommand : MessageCommand
    {
        public CreateNewLocationCommand(long vehicleId, long deviceId, 
            double latitude, double longitude)
        {
            VehicleId = vehicleId;
            DeviceId = deviceId;
            Latitude = latitude;
            Longitude = longitude;          
        }      
    }
}
