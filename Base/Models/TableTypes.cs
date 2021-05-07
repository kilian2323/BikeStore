using Core.Models.Tables;
using System;

namespace Core.Models
{
    /* Defines all tables used in this UI */
    public class TableTypes
    {

        /* Table type getter */

        public static Type GetTypeFromAlias(string alias)
        {
            if (alias.Equals("Customers"))
            {
                return typeof(Customer);
            }
            if (alias.Equals("Staff"))
            {
                return typeof(Staff);
            }
            if (alias.Equals("Orders"))
            {
                return typeof(Order);
            }
            if (alias.Equals("Stocks"))
            {
                return typeof(Stock);
            }
            return null;
        }
    }
}
