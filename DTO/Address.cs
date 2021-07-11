namespace kontacto_api.DTO
{
    public class AddressDTO
    {
        public string Address { get; set; }
        public string SecondAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}