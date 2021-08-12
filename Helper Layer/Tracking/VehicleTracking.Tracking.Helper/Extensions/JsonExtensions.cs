using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace VehicleTracking.Tracking.Helper.Extensions
{
    public static class JsonExtensions
    {
        public static string Serialize<TType>(this TType instance)
        {
            var serializer = new DataContractJsonSerializer(typeof(TType));
            using var stream = new MemoryStream();
            serializer.WriteObject(stream, instance);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static TType Deserialize<TType>(this string json)
        where TType : class
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var serializer = new DataContractJsonSerializer(typeof(TType));
            return serializer.ReadObject(stream) as TType;
        }
    }

}
