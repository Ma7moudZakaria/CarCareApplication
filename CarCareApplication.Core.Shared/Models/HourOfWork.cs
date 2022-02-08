using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class HourOfWork : BaseEntity
    {
        public int DayId { get; set; }
        public int HourId { get; set; }
        public int AvailableMinutes { get; set; }
        public bool IsForcedEnabled { get; set; }
        [ForeignKey(nameof(DayId))] public Day Day { get; set; }
        [ForeignKey(nameof(HourId))] public Hour Hour { get; set; }
    }
}
