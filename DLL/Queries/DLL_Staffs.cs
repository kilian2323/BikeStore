using Core.Classes;
using Core.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DLL.Queries
{
    public class DLL_Staffs : DLL_Base
    {
        public List<Staff> GetRows(Search search)
        {
            string commandString = @"USE BikeStores;

                                SELECT
                                   staff.[staff_id] AS [staff_id]
                                  ,staff.[first_name] AS [first_name]
                                  ,staff.[last_name] AS [last_name]
                                  ,staff.[email] AS [email]
                                  ,staff.[phone] AS [phone]
                                  ,staff.[active] AS [active]
                                  ,staff.[store_id] AS [store_id]
                                  ,store.[store_name] AS [store_name]
                                  ,store.[city] AS [store_city]
                                  ,store.[phone] AS [store_phone]
                                  ,store.[email] AS [store_email]
                                  ,staff.[manager_id] AS [manager_id]
                                  ,manager.[first_name] AS [manager_first_name]
                                  ,manager.[last_name] AS [manager_last_name]
								  ,CASE WHEN num.[no. of employees] IS NOT NULL THEN 'yes' ELSE 'no'
								   END AS [staff_ismanager]

                             FROM [sales].[staffs] staff
                               LEFT JOIN [sales].[staffs] manager ON staff.[manager_id] = manager.[staff_id]
                               LEFT JOIN [sales].[stores] store ON staff.[store_id] = store.[store_id]
							   LEFT JOIN [dbo].[Staff_and_number_of_employees_under] num ON [staff].[staff_id] = num.[staff_id]";

            if (search.filter == true)
            {
                Debug.WriteLine("SearchFilter is true");
                commandString += GetFilterString(search,"staff");
                commandString += @" GROUP BY staff.[staff_id], staff.[first_name], staff.[last_name], staff.[email], staff.[phone],
							         staff.[active], staff.[store_id], store.[store_name], store.[city], store.[phone], store.[email],
									 staff.[manager_id], manager.[first_name], manager.[last_name], num.[no. of employees]";
            }

            Debug.WriteLine(commandString);
            
            cnn = new SqlConnection(connectionString);
            List<Staff> staffList = new List<Staff>();
            using (cnn)
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(commandString, cnn);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var staff = new Staff();
                    try
                    {
                        staff.StaffID = (int)dataReader["staff_id"];
                        staff.FirstName = dataReader["first_name"].ToString();
                        staff.LastName = dataReader["last_name"].ToString();
                        staff.Email = dataReader["email"].ToString();
                        staff.Phone = dataReader["phone"].ToString();
                        staff.IsActive = (bool)dataReader["active"];
                        staff.StoreID = (int)dataReader["store_id"];
                        staff.StoreName = dataReader["store_name"].ToString();
                        staffList.Add(staff);
                    }
                    catch (Exception exception)
                    {
                    }
                }
                cnn.Close();
            }
            return staffList;
        }

        public Staff QueryLogin(string username, string password)
        {
            Debug.WriteLine("DLL_Staffs.QueryLogin()");
            string commandString = @"USE BikeStores;
                            SELECT
                                   [staff_id]
                                  ,[first_name]
                                  ,[last_name]
                                  ,[email]   
                                  ,[phone]
                                  ,[password]
                                  ,[active]
                            FROM [BikeStores].[sales].[staffs]
                            WHERE [email] = '" + username + "' AND [password] = '" + password + "';";
            cnn = new SqlConnection(connectionString);
            Staff loggingInEmployee = new Staff();
            using (cnn)
            {
                cnn.Open();
                SqlCommand command = new SqlCommand(commandString, cnn);
                SqlDataReader dataReader = command.ExecuteReader();                
                bool found = false;
                while (dataReader.Read() && !found)
                {
                    found = true; // we can stop reading now, assuming that there is only one entry with this username/password
                    try
                    {
                        loggingInEmployee.StaffID = (int)dataReader["staff_id"];
                        loggingInEmployee.FirstName = dataReader["first_name"].ToString();
                        loggingInEmployee.LastName = dataReader["last_name"].ToString();
                        loggingInEmployee.Email = dataReader["email"].ToString();
                        loggingInEmployee.Phone = dataReader["phone"].ToString();
                        loggingInEmployee.IsActive = Convert.ToBoolean(dataReader["active"]);
                        loggingInEmployee.Password = dataReader["password"].ToString();
                    }
                    catch (Exception exception)
                    {
                    }
                }
                cnn.Close();
            }
            return loggingInEmployee;
        }
    }
}
