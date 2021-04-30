using Core.Classes;
using Core.Models;
using DLL.Queries;
using System.Collections.Generic;
using System.Diagnostics;

namespace BLL.Tables
{
    public class BLL_Staffs
    {
        public delegate void BLLStaffViolation(string error);
        public static event BLLStaffViolation OnBLLStaffViolation;

        private DLL_Staffs dLL_Staffs = new DLL_Staffs();

        public List<Staff> GetRows(Search search)
        {
            return dLL_Staffs.GetRows(search);
        }

        /* Issues a login query on the staffs table */
        public Staff DoLogin(string username, string password)
        {
            
            /* Check here only for null or empty, then pass the strings
             * on to the DLL and assemble the query there
             */
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Debug.WriteLine("OnBLLStaffViolation triggered");
                OnBLLStaffViolation.Invoke("Username and password must not be empty!");
                return null;
            }

            Staff employee = dLL_Staffs.QueryLogin(username, password);

            /* After retrieving the result from DLL, check if the employee is active.
             * If not, do not log in
             */

            if (employee != null && employee.IsActive == true)
            {
                return employee;
            }
            OnBLLStaffViolation?.Invoke("Username/password wrong, user not found or insufficient privileges.");
            return null;
        }
    }
}
