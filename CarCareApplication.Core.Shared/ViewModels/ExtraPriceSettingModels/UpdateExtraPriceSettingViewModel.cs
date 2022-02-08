using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels
{
    public class UpdateExtraPriceSettingViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "ExtraPriceSetting Id is required"), DataType(DataType.Text)] public int Id { get; set; }
        [JsonPropertyName("extraPrice"), Required(ErrorMessage = "ExtraPrice is required")] public float ExtraPrice { get; set; }
        [JsonPropertyName("serviceId"), Required(ErrorMessage = "Service Id is required")] public int ServiceId { get; set; }
        [JsonPropertyName("cartypeId"), Required(ErrorMessage = "CarType Id is required")] public int CarTypeId { get; set; }
    }
}
