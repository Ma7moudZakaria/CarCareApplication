using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class Transaction : BaseEntity
    {
        public int CarTypeId { get; set; }
        public int ServiceId { get; set; }
        public int AddressId { get; set; }
        public int HourOfWorkId { get; set; }
        public float BasePrice { get; set; }
        public bool IsWeekly { get; set; }
        [ForeignKey(nameof(AddressId))] public Address Address { get; set; }
        [ForeignKey(nameof(ServiceId))] public Service Service { get; set; }
        [ForeignKey(nameof(CarTypeId))] public CarType CarType { get; set; }
        [ForeignKey(nameof(HourOfWorkId))] public HourOfWork HourOfWork { get; set; }
    }
}
