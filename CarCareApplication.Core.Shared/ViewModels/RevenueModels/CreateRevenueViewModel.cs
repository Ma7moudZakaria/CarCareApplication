using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.RevenueModels
{
    public class CreateRevenueViewModel
    {
        [JsonPropertyName("cash"), Required(ErrorMessage = "Cash is required")] public float Cash { get; set; }
        [JsonPropertyName("description"), Required(ErrorMessage = "Description is required"), DataType(DataType.Text)] public string Description { get; set; }
    }
}
