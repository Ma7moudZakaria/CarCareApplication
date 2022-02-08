using System;

namespace CarCareApplication.Core.Shared.Models
{
    public class Day : BaseEntity
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
