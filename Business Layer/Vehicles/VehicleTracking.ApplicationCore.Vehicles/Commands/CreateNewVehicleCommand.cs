namespace VehicleTracking.ApplicationCore.Vehicles.Commands
{
    public class CreateNewVehicleCommand : MessageCommand
    {
        public CreateNewVehicleCommand(string licensePlateNumber, string vehicleName, string brand, string description, string color, 
            string model, string bodyType, string maker, string year, string country)
        {
            LicensePlateNumber = licensePlateNumber;
            VehicleName = vehicleName;
            Brand = brand;
            Description = description;
            Color = color;
            Model = model;
            BodyType = bodyType;
            Maker = maker;
            Year = year;
            Country = country;
        }
    }
}
