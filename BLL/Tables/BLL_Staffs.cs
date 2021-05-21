using Core.Classes;
using Core.Models.Tables;
using DLL.Queries;
using System.Collections.Generic;

namespace BLL.Tables
{
    public class BLL_Staffs
    {
        private DLL_Staffs dLL_Staffs = new DLL_Staffs();

        public List<Staff> GetRows(Search search)
        {
            return dLL_Staffs.GetRows(search);
        }

        /* Issues a login query on the staffs table */
        public Staff DoLogin(string username, string password)
        {
            Staff employee = dLL_Staffs.QueryLogin(username, password);

            /* After retrieving the result from DLL, check if the employee is active.
             * If not, do not log in
             */
            if (employee != null && employee.IsActive == true)
            {
                return employee;
            }
            return null;
        }
    }
}
