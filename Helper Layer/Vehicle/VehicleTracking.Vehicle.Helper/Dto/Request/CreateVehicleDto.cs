namespace VehicleTracking.Vehicle.Helper.Dto.Request
{
    public class CreateVehicleDto
    {
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

        public CreateVehicleDto() { }

        public CreateVehicleDto(string licensePlateNumber, string model,string country,
            string vehicleName, string maker, string year,
            string bodyType, string brand, string color, string description)
        {
            LicensePlateNumber = licensePlateNumber;
            Model = model;
            Country = country;
            VehicleName = vehicleName;
            Maker = maker;
            Year = year;
            BodyType = bodyType;
            Brand = brand;
            Color = color;
            Description = description;
        }
    }
}
