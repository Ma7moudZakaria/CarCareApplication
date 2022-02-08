using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarCareApplication.Core.Shared.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))] public Role Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
