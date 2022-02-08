using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.CarTypeModels
{
    public class IndexCarTypeViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
    }
}
