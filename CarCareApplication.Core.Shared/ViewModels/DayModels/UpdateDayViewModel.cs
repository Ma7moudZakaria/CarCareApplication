using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.DayModels
{
    public class UpdateDayViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Day Id is required")] public int Id { get; set; }
        [JsonPropertyName("nameAr")] public string NameAR { get; set; }
        [JsonPropertyName("nameEn")] public string NameEN { get; set; }
        [JsonPropertyName("isEnabled")] public bool IsEnabled { get; set; }
    }
}
