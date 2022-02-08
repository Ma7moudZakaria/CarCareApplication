using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.SettingModels
{
    public class CheckAreaSupportViewModel
    {
        [JsonPropertyName("serviceId")]
        public int ServiceId { get; set; }
        [JsonPropertyName("cartypeId")]
        public int CarTypeId { get; set; }
        [JsonPropertyName("kilometerAway")]
        public float KilometerAway { get; set; }

    }
}
