using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.SettingModels
{
    public class IndexSettingViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("kilometerRate")] public float KilometerRate { get; set; }
        [JsonPropertyName("kilometerMin")] public float KilometerMin { get; set; }
        [JsonPropertyName("kilometerMax")] public float KilometerMax { get; set; }
        [JsonPropertyName("service")] public string Service { get; set; }
        [JsonPropertyName("cartype")] public string CarType { get; set; }
    }
}
