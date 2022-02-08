using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels
{
    public class IndexHourOfWorkViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
        [JsonPropertyName("isForcedEnabled")]
        public bool IsForcedEnabled { get; set; }

    }
}
