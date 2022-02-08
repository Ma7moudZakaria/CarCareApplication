using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.RevenueModels
{
    public class UpdateRevenueViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Revenue Id is required"), DataType(DataType.Text)] public int Id { get; set; }
        [JsonPropertyName("cash")] public float Cash { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
    }
}
