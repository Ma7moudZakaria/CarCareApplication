using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class ExtraPriceSetting : BaseEntity
    {
        public float ExtraPrice { get; set; }

        public int ServiceId { get; set; }
        public int CarTypeId { get; set; }

        [ForeignKey(nameof(CarTypeId))] public CarType CarType { get; set; }
        [ForeignKey(nameof(ServiceId))] public Service Service { get; set; }
    }
}
