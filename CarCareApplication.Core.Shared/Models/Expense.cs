using System;

namespace CarCareApplication.Core.Shared.Models
{
    public class Expense : BaseEntity
    {
        public float Cash { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow.AddHours(2);
        public CashType CashType { get; set; }
    }
}
