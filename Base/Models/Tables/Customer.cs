using Core.Classes;

namespace Core.Models.Tables
{
    public class Customer
    {

        /* Table columns */

        [ColumnViewName("ID")]
        [ColumnDBName("customer_id")]
        public int CustomerID { get; set; }
        
        [ColumnViewName("First name")]
        [ColumnDBName("first_name")]
        public string FirstName { get; set; }
        
        [ColumnViewName("Last name")]
        [ColumnDBName("last_name")]
        public string LastName { get; set; }
        
        [ColumnViewName("Phone")]
        [ColumnDBName("phone")]
        public string Phone { get; set; }
        
        [ColumnViewName("E-mail")]
        [ColumnDBName("email")]
        public string Email { get; set; }
        
        [ColumnViewName("Street")]
        [ColumnDBName("street")]
        public string Street { get; set; }
        
        [ColumnViewName("ZIP code")]
        [ColumnDBName("zip_code")]
        public string ZIPcode { get; set; }
        
        [ColumnViewName("City")]
        [ColumnDBName("city")]
        public string City { get; set; }
        
        [ColumnViewName("State")]
        [ColumnDBName("state")]
        public string State { get; set; }


        
    }
}
