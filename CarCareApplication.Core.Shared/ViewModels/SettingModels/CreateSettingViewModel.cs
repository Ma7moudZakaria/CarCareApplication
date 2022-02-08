using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.SettingModels
{
    public class CreateSettingViewModel
    {
        [JsonPropertyName("kilometerRate"), Required(ErrorMessage = "Kilometer Rate is required")] public float KilometerRate { get; set; }
        [JsonPropertyName("kilometerMin"), Required(ErrorMessage = "Kilometer Min is required")] public float KilometerMin { get; set; }
        [JsonPropertyName("kilometerMax"), Required(ErrorMessage = "Kilometer Max is required")] public float KilometerMax { get; set; }
        [JsonPropertyName("serviceId"), Required(ErrorMessage = "Service Id is required")] public int ServiceId { get; set; }
        [JsonPropertyName("cartypeId"), Required(ErrorMessage = "Car type Id is required")] public int CarTypeId { get; set; }
    }
}
