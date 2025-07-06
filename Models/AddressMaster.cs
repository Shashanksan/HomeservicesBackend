
namespace Final_Project.Models
{
    public class AddressMaster
    {
        public string AddressId { get; set; }  // Primary Key
        public string UserId { get; set; }     // Foreign Key / User Identifier
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Ward { get; set; }
        public string Landmark { get; set; }
        public string Street { get; set; }
        public string FullAddress { get; set; }
    }
}
