using Final_Project.Enums;

namespace Final_Project.Models
{
    public class RegistractionMaster
    {
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Role JobRole { get; set; }

        public string UserPhoneNumber { get; set; }
        public string UserAddress { get; set; }

    }
}
