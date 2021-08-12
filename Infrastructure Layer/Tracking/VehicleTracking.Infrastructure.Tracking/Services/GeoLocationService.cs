using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTracking.Infrastructure.Tracking.Interfaces;
using VehicleTracking.Tracking.Helper.Dto.Response;
using VehicleTracking.Tracking.Helper.Extensions.Utilities;
using VehicleTracking.Tracking.Helper.Extensions;

namespace VehicleTracking.Infrastructure.Tracking.Services
{
    public class GeoLocationService : IGeoLocation
    {
        private readonly HttpClient _client;
        private readonly GeoLocationConfigurations _trackingAppSettings;
        public GeoLocationService(IOptions<GeoLocationConfigurations> trackingAppSettings)
        {
            _trackingAppSettings = trackingAppSettings.Value;

            _client = new HttpClient
            {
                BaseAddress = new Uri(_trackingAppSettings.getLocationBaseUrl),
            };
        }
        public async Task<GeoLocationResponseDto> GetLocation(double latitude, double longitude)
        {
            var response = new GeoLocationResponseDto();

            response.responseCode = "01";
            response.message = "Failed";

            try
            {
                var request = await _client.GetAsync($"{_trackingAppSettings.getLocationUrlExtenstion}" +
                $"{"?key="}{_trackingAppSettings.geoLocationKey}" +
                $"{"&lat="}{latitude}" +
                $"{"&lon="}{longitude}" +
                $"{"&format="}{_trackingAppSettings.requestFormat}");

                if (!request.IsSuccessStatusCode)
                    return response;

                var content = await request.Content.ReadAsStringAsync();
                response = content.Deserialize<GeoLocationResponseDto>();

              //  var content = await request.Content.ReadAsStringAsync();

               // response = JsonConvert.DeserializeObject<GeoLocationResponseDto>(content);

                response.responseCode = "00";
                response.message = "Success";

                return response;
            }
            catch (Exception)
            {
                return new GeoLocationResponseDto { responseCode = "02", message = "Internal error occured"};
            }
        }
    }
}
