namespace Final_Project.Models
{
    public class OrderMaster 
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public string LabourID { get; set; }
        public string ServiceID { get; set; }
        public string AddressId { get; set; }
        public decimal Discount { get; set; }
        public decimal SurviceCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
