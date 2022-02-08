namespace CarCareApplication.Resources
{
    public class Request
    {
        public static Request Current = new Request();

        public int ServiceId { get; set; }
        public int HourOfWorkId { get; set; }
        public int CarTypeId { get; set; }
        public int AddressId { get; set; }
        public string CarType { get; set; }
        public string HourOfWork { get; set; }
        public string Service { get; set; }
        public string Address { get; set; }
        public float Price { get; set; }
        public float DeliveryPrice { get; set; }
    }
}
