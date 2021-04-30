using Core.Classes;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DLL.Queries;
using System.Diagnostics;

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
