using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.AddressModels
{
    public class CreateAddressViewModel
    {
        [JsonPropertyName("name"), Required(ErrorMessage = "Address Name is required"), DataType(DataType.Text)]
        public string Name { get; set; }

        [JsonPropertyName("type"), Required(ErrorMessage = "Address Type is required"), DataType(DataType.Text)]
        public string Type { get; set; }

        [JsonPropertyName("fullAddress"), Required(ErrorMessage = "Full Address is required"), DataType(DataType.Text)]
        public string FullAddress { get; set; }

        [JsonPropertyName("phoneNumber"), Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("latitude"), Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude"), Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        [JsonPropertyName("kilometersAway"), Required(ErrorMessage = "Kilometers Away is required")]
        public float KilometersAway { get; set; }

        [JsonPropertyName("userid"), Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }
    }
}
