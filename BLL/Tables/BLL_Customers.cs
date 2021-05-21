using Core.Classes;
using Core.Models.Tables;
using DLL.Queries;
using System.Collections.Generic;

namespace BLL.Tables
{
    public class BLL_Customers
    {
        private DLL_Customers dLL_Customers = new DLL_Customers();

        public List<Customer> GetRows(Search search)
        {            
            return dLL_Customers.GetRows(search);
        }
    }
}
