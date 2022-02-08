using System;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.RevenueModels
{
    public class IndexRevenueViewModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("cash")] public float Cash { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("date")] public DateTime Date { get; set; }
    }
}
