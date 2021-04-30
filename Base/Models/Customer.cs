using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Models
{
    public class Customer
    {
        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new Collection<Order>();
        }

        /* Table columns */

        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZIPcode { get; set; }
    }
}
