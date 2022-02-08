using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.SettingModels
{
    public class UpdateSettingViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Setting Id is required")] public int Id { get; set; }
        [JsonPropertyName("kilometerRate"), Required(ErrorMessage = "Kilometer Rate is required")] public float KilometerRate { get; set; }
        [JsonPropertyName("kilometerMin"), Required(ErrorMessage = "Kilometer Min is required")] public float KilometerMin { get; set; }
        [JsonPropertyName("kilometerMax"), Required(ErrorMessage = "Kilometer Max is required")] public float KilometerMax { get; set; }
        [JsonPropertyName("serviceId"), Required(ErrorMessage = "Service Id is required")] public int ServiceId { get; set; }
        [JsonPropertyName("caryypeId"), Required(ErrorMessage = "Car Type Id is required")] public int CarTypeId { get; set; }
    }
}
