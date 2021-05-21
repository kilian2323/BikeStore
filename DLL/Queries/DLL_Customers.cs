using Core.Classes;
using Core.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DLL.Queries
{
    public class DLL_Customers : DLL_Base
    {
        public List<Customer> GetRows(Search search)
        {
            string commandString = @"SELECT
                                   [sales].[customers].[customer_id] AS [customer_id]
                                  ,[sales].[customers].[first_name] AS [first_name]
                                  ,[sales].[customers].[last_name] AS [last_name]
                                  ,[sales].[customers].[email] AS [email]
                                  ,[sales].[customers].[phone] AS [phone]
                                  ,[sales].[customers].[street] AS [street]
                                  ,[sales].[customers].[city] AS [city]
                                  ,[sales].[customers].[state] AS [state]
                                  ,[sales].[customers].[zip_code] AS [zip_code]
                             FROM [sales].[customers] ";

            if (search.filter == true)
            {
                commandString += GetFilterString(search, "sales.customers");
                commandString += @" GROUP BY [sales].[customers].[customer_id], [sales].[customers].[first_name], [sales].[customers].[last_name], [sales].[customers].[email], [sales].[customers].[phone],
							         [sales].[customers].[street],[sales].[customers].[city], [sales].[customers].[state], [sales].[customers].[zip_code]";
            }
            Debug.WriteLine(commandString);
            cnn = new SqlConnection(connectionString);
            var customerList = new List<Customer>();
            using (cnn)
            {
                cnn.Open();
                var command = new SqlCommand(commandString, cnn);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var customer = new Customer();
                    try
                    {
                        customer.CustomerID = (int)dataReader["customer_id"];
                        customer.FirstName = dataReader["first_name"].ToString();
                        customer.LastName = dataReader["last_name"].ToString();
                        customer.Email = dataReader["email"].ToString();
                        customer.Phone = dataReader["phone"].ToString();
                        customer.Street = dataReader["street"].ToString();
                        customer.City = dataReader["city"].ToString();
                        customer.State = dataReader["state"].ToString();
                        customer.ZIPcode = dataReader["zip_code"].ToString();
                        customerList.Add(customer);
                    }
                    catch (Exception exception)
                    {
                    }
                }
                cnn.Close();
            }
            Debug.WriteLine("List of customers contains " + customerList.Count + " entries");
            return customerList;
        }
    }
}
