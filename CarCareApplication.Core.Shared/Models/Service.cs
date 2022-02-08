namespace CarCareApplication.Core.Shared.Models
{
    public class Service : BaseEntity
    {
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }
        public float BasePrice { get; set; }
        public int ServingMinutes { get; set; }
    }
}
