using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.TransactionModels
{
    // Retun list of Transaction grouped by date their status is enabled. (Car Type , Service Type , Kilometers away, Total Cash, Service Time).

    public class IndexTransactionViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("carType")] public string CarType { get; set; }
        [JsonPropertyName("serviceType")] public string ServiceType { get; set; }
        [JsonPropertyName("kilometersAway")] public float KilometersAway { get; set; }
        [JsonPropertyName("basePrice")] public float BasePrice { get; set; }
        [JsonPropertyName("serviceTime")] public string ServiceTime { get; set; }
    }
}
