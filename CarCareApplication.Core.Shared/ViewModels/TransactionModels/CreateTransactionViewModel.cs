using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.TransactionModels
{
    public class CreateTransactionViewModel
    {
        [JsonPropertyName("hourOfWorkId"), Required(ErrorMessage = "Hour Of Work Id is required")] public int HourOfWorkId { get; set; }
        [JsonPropertyName("serviceId"), Required(ErrorMessage = "Service Id is required")] public int ServiceId { get; set; }
        [JsonPropertyName("carTypeId"), Required(ErrorMessage = "Car Type Id is required")] public int CarTypeId { get; set; }
        [JsonPropertyName("addressId"), Required(ErrorMessage = "Address Id is required")] public int AddressId { get; set; }
        [JsonPropertyName("basePrice"), Required(ErrorMessage = "Base Price is required")] public float BasePrice { get; set; }
    }
}
