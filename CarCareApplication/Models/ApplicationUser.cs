namespace CarCareApplication.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public UserType UserType { get; set; }
        public bool IsUserLoggedIn { get; set; }
    }
    public enum UserType
    {
        Admin, Customer, Driver , None
    }
}
