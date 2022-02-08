using System.ComponentModel.DataAnnotations;

namespace CarCareApplication.Core.Shared.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsEnabled { get; set; }
    }
}
