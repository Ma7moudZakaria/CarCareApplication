using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.ExpensesModels
{
    public class UpdateExpenseViewModel
    {
        [JsonPropertyName("id"), Required(ErrorMessage = "Expense Id is required"), DataType(DataType.Text)] public int Id { get; set; }
        [JsonPropertyName("cash")] public float Cash { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
    }
}
