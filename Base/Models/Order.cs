namespace Core.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public bool CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public int OrderStatus { get; set; }

        public string OrderDate { get; set; }

        public string RequiredDate { get; set; }

        public string ShippedDate { get; set; }

        public int StoreID { get; set; }

        public int StoreName { get; set; }

        public string StoreCity { get; set; }

        public string StorePhone { get; set; }

        public string StoreEmail { get; set; }

        public int StaffID { get; set; }

        public string StaffFirstName { get; set; }

        public string StaffLastName { get; set; }

        public string StaffEmail { get; set; }
    }
}
