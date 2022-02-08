using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class Address : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float KilometersAway { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))] public User User { get; set; }
    }
}
