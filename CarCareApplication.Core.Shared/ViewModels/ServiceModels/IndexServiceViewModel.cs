using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.ServiceModels
{
    public class IndexServiceViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("servingMinutes")] public int ServingMinutes { get; set; }
        [JsonPropertyName("basePrice")] public float BasePrice { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
        [JsonPropertyName("description")]  public string Description { get; set; }

    }
}
