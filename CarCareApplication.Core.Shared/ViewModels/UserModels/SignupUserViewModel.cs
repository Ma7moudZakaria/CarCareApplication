using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.UserModels
{
    public class SignupUserViewModel
    {
        [JsonPropertyName("firstName"), Required(ErrorMessage = "First Name is required"), DataType(DataType.Text)]
        public string FirstName { get; set; }

        [JsonPropertyName("secondName"), Required(ErrorMessage = "Second Name is required"), DataType(DataType.Text)]
        public string SecondName { get; set; }

        [JsonPropertyName("phoneNumber"), Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("password"), Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [JsonPropertyName("roleId"), Required]
        public int RoleId { get; set; }
    }
}
