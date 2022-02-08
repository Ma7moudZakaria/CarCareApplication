using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarCareApplication.Core.Shared.ViewModels.UserModels
{
    public class SigninUserViewModel
    {
        [JsonPropertyName("phoneNumber"), Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("password"), Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
