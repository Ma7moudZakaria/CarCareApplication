using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.TransactionModels
{
    public class TransactionDetailViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("carType")]
        public string CarType { get; set; }
        [JsonPropertyName("serviceType")]
        public string ServiceType { get; set; }
        [JsonPropertyName("kilometersAway")]
        public float KilometersAway { get; set; }
        [JsonPropertyName("basePrice")]
        public float BasePrice { get; set; }
        [JsonPropertyName("serviceTime")]
        public string ServiceTime { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("fullAddress")]
        public string FullAddress { get; set; }
    }
}
