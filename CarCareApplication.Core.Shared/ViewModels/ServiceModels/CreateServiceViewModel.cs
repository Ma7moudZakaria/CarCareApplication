using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.ServiceModels
{
    public class CreateServiceViewModel
    {
        [JsonPropertyName("nameAr"), Required(ErrorMessage = "Arabic Name is required"), DataType(DataType.Text)] public string NameAR { get; set; }
        [JsonPropertyName("nameEn"), Required(ErrorMessage = "English Name is required"), DataType(DataType.Text)] public string NameEN { get; set; }
        [JsonPropertyName("descriptionEn"), Required(ErrorMessage = "English Description is required"), DataType(DataType.Text)] public string DescriptionEN { get; set; }
        [JsonPropertyName("descriptionAr"), Required(ErrorMessage = "Arabic Description is required"), DataType(DataType.Text)] public string DescriptionAR { get; set; }
        [JsonPropertyName("servingMinutes"), Required(ErrorMessage = "Serving Minutes is required")] public int ServingMinutes { get; set; }
        [JsonPropertyName("basePrice"), Required(ErrorMessage = "Base Price is required")] public float BasePrice { get; set; }
        [JsonPropertyName("isEnabled"), Required(ErrorMessage = "IsEnabled is required")] public bool IsEnabled { get; set; }
    }
}
