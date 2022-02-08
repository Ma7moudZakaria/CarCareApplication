using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels
{
    public class CreateHourOfWorkViewModel
    {
        [JsonPropertyName("dayId"), Required(ErrorMessage = "Day Id is required")]
        public int DayId { get; set; }

        [JsonPropertyName("hourId"), Required(ErrorMessage = "Hour Id is required")]
        public int HourId { get; set; }

        [JsonPropertyName("date"), Required(ErrorMessage = "Date is required"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [JsonPropertyName("availableMinutes"), Required(ErrorMessage = "Available Minutes is required")]
        public int AvailableMinutes { get; set; }

        [JsonPropertyName("isForcedEnabled")]
        public bool IsForcedEnabled { get; set; }

        [JsonPropertyName("isEnabled"), Required(ErrorMessage = "Is Enabled is required")] public bool IsEnabled { get; set; }
    }
}
