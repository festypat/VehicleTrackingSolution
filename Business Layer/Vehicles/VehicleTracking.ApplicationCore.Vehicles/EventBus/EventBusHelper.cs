namespace VehicleTracking.ApplicationCore.Vehicles.EventBus
{
    public static class EventBusHelper
    {
        public static string GetTypeName<T>()
        {
            var name = typeof(T).FullName.ToLower().Replace("+", ".");

            if (typeof(T) == typeof(IEvent))
            {
                name += "_event";
            }
            else if (typeof(T) == typeof(ICommand))
            {
                name += "_command";
            }

            return name;
        }
    }

}
