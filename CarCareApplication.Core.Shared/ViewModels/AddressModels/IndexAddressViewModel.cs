using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.AddressModels
{
    public class IndexAddressViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("fullAddress")]
        public string FullAddress { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("kilometerAway")]
        public float KilometerAway { get; set; }
    }
}
