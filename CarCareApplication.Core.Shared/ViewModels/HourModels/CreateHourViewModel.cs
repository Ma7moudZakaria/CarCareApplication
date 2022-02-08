using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.HourModels
{
    public class CreateHourViewModel
    {
        [JsonPropertyName("nameAr"), Required(ErrorMessage = "Arabic Name is required"), DataType(DataType.Text)] public string NameAR { get; set; }

        [JsonPropertyName("nameEn"), Required(ErrorMessage = "English Name is required"), DataType(DataType.Text)] public string NameEN { get; set; }

        [JsonPropertyName("timeStart"), Required(ErrorMessage = "Time Start is required")] public TimeSpan TimeStart { get; set; }

        [JsonPropertyName("timeEnd"), Required(ErrorMessage = "Time End is required")] public TimeSpan TimeEnd { get; set; }
    }
}
