using RawRabbit.Configuration;

namespace VehicleTracking.ApplicationCore.Vehicles.EventBus.RabbitMQ
{
    public class RabbitMQOptions : RawRabbitConfiguration
    {
        public QueueOptions QueueOption { get; set; }
        public ExchangeOptions ExchangeOption { get; set; }

        public class QueueOptions : GeneralQueueConfiguration
        {
            public string Name { get; set; }
        }

        public class ExchangeOptions : GeneralExchangeConfiguration
        {
            public string Name { get; set; }
        }
    }

}
