using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels
{
    public class UpdateHourOfWorkViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Hour Of Work is required")]
        public int Id { get; set; }

        [JsonPropertyName("dayId")]
        public int DayId { get; set; }

        [JsonPropertyName("hourId")]
        public int HourId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("availableMinutes")]
        public int AvailableMinutes { get; set; }
        [JsonPropertyName("isEnabled")]
        public bool IsEnabled { get; set; }
        [JsonPropertyName("isForcedEnabled")]
        public bool IsForcedEnabled { get; set; }
    }
}
