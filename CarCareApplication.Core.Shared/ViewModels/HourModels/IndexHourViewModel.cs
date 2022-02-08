using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourModels
{
    public class IndexHourViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
    }
}
