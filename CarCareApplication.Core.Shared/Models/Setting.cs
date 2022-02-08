using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class Setting : BaseEntity
    {
        public float KilometerRate { get; set; }
        public float KilometerMin { get; set; }
        public float KilometerMax { get; set; }
        public int CarTypeId { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey(nameof(CarTypeId))] public CarType CarType { get; set; }
        [ForeignKey(nameof(ServiceId))] public Service Service { get; set; }
    }
}
