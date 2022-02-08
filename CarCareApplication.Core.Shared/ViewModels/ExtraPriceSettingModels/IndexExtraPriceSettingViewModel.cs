using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels
{
    public class IndexExtraPriceSettingViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("extraPrice")] public float ExtraPrice { get; set; }
        [JsonPropertyName("service")] public string Service { get; set; }
        [JsonPropertyName("cartype")] public string CarType { get; set; }
    }
}
