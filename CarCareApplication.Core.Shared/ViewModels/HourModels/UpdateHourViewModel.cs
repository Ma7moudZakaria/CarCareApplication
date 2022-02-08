using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourModels
{
    public class UpdateHourViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Hour Id is required")] public int Id { get; set; }
        [JsonPropertyName("nameAr")] public string NameAR { get; set; }

        [JsonPropertyName("nameEn")] public string NameEN { get; set; }

        [JsonPropertyName("timeStart")] public TimeSpan TimeStart { get; set; }

        [JsonPropertyName("timeEnd")] public TimeSpan TimeEnd { get; set; }
    }
}
