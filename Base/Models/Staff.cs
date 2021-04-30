using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Models
{
    public class Staff
    {
        public ICollection<Order> Orders { get; set; }
        
        public Staff()
        {
            Orders = new Collection<Order>();
        }

        /* Table columns */
        
        public int StaffID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }        

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public int StoreID { get; set; }

        public string StoreName { get; set; }

        public string StoreCity { get; set; }

        public string StorePhone { get; set; }

        public string StoreEmail { get; set; }

        public bool IsManager { get; set; }

        public int ManagerID { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string PasswordHash { get; set; } // not retrievable

        public string Password { get; set; }     // not retrievable
    }
}
