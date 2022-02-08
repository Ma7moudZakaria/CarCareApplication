using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels
{
    public class DetailHourOfWorkViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("hourId")]
        public int HourId { get; set; }
        [JsonPropertyName("dayId")]
        public int DayId { get; set; }
        [JsonPropertyName("availableMinutes")]
        public int AvailableMinutes { get; set; }
        [JsonPropertyName("isForcedEnabled")]
        public bool IsForcedEnabled { get; set; }
    }
}
