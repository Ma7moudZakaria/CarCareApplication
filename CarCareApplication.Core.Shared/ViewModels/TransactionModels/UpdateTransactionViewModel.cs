using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.TransactionModels
{
    public class UpdateTransactionViewModel
    {
        [JsonPropertyName("serviceId")] public int ServiceId { get; set; }
        [JsonPropertyName("hourOfWorkId")] public int HourOfWorkId { get; set; }
        [JsonPropertyName("carTypeId")] public int CarTypeId { get; set; }
        [JsonPropertyName("addressId")] public int AddressId { get; set; }
        [JsonPropertyName("basePrice")] public float BasePrice { get; set; }
    }
}
