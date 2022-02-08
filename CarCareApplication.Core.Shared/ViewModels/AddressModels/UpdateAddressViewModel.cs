using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.AddressModels
{
    public class UpdateAddressViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Address id is required")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("fullAddress")]
        public string FullAddress { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("kilometersAway")]
        public float KilometersAway { get; set; }

        [JsonPropertyName("userid")]
        public int UserId { get; set; }
    }
}
