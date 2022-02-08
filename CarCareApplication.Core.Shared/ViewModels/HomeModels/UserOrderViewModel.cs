using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HomeModels
{
    public class UserOrderViewModel
    {
        [JsonPropertyName("carTypeId")]
        public int CarTypeId { get; set; }
        [JsonPropertyName("serviceId")]
        public int ServiceId { get; set; }
        [JsonPropertyName("hourOfWorkId")]
        public int HourOfWorkId { get; set; }
        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }
        [JsonPropertyName("isWeekly")]
        public bool IsWeekly { get; set; }
    }
}
