using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.DayModels
{
    public class CreateDayViewModel
    {
        [JsonPropertyName("nameAr"), Required(ErrorMessage = "Arabic Name is required"), DataType(DataType.Text)] public string NameAR { get; set; }
        [JsonPropertyName("nameEn"), Required(ErrorMessage = "English Name is required"), DataType(DataType.Text)] public string NameEN { get; set; }
        [JsonPropertyName("isEnabled"), Required(ErrorMessage = "Is Enabled is required")] public bool IsEnabled { get; set; }
    }
}
