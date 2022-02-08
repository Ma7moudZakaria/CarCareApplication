using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.DayModels
{
    public class IndexDayViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
    }
}
