using System;

namespace CarCareApplication.Core.Shared.Models
{
    public class Hour : BaseEntity
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
