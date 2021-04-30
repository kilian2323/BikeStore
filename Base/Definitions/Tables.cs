using Core.Classes;
using Core.Models;
using System.Collections.Generic;

namespace Core.Definitions
{
    public class Tables
    {
        private static Dictionary<string, Table> allTables { get; set; } = new Dictionary<string, Table>();

        public static void GenerateTables()
        {
            /**
             * These definitions must correspond with Models!
             */
            
            /* Table Customers */

            Table sc = new Table("sales", "customers", typeof(Customer));
            sc.AddColumn(new ColumnDirect("customer_id", "CustomerID", "ID", typeof(int)));
            sc.AddColumn(new ColumnDirect("first_name", "FirstName", "First name", typeof(string)));
            sc.AddColumn(new ColumnDirect("last_name", "LastName", "Last name", typeof(string)));
            sc.AddColumn(new ColumnDirect("phone", "Phone", "Phone", typeof(string)));
            sc.AddColumn(new ColumnDirect("email", "Email", "E-mail", typeof(string)));
            sc.AddColumn(new ColumnDirect("street", "Street", "Street", typeof(string)));
            sc.AddColumn(new ColumnDirect("city", "City", "City", typeof(string)));
            sc.AddColumn(new ColumnDirect("state", "State", "State", typeof(string)));
            sc.AddColumn(new ColumnDirect("zip_code", "ZIPcode", "ZIP code", typeof(string)));

            /* Table Staff */

            Table ss = new Table("sales", "staffs", typeof(Staff));
            ss.AddColumn(new ColumnDirect("staff_id", "StaffID", "ID", typeof(int)));
            ss.AddColumn(new ColumnDirect("first_name", "FirstName", "First name", typeof(string)));
            ss.AddColumn(new ColumnDirect("last_name", "LastName", "Last name", typeof(string)));
            ss.AddColumn(new ColumnDirect("email", "Email", "E-mail", typeof(string)));
            ss.AddColumn(new ColumnDirect("phone", "Phone", "Phone", typeof(string)));
            ss.AddColumn(new ColumnDirect("active", "IsActive", "Active", typeof(bool)));
            ss.AddColumn(new ColumnDirect("store_id", "StoreID", "Store ID", typeof(int)));
            ss.AddColumn(new ColumnDerived("store_name", "StoreName", "Store name", typeof(string)));
            ss.AddColumn(new ColumnDerived("store_city", "StoreCity", "Store city", typeof(string)));
            ss.AddColumn(new ColumnDerived("store_phone", "StorePhone", "Store phone", typeof(string)));
            ss.AddColumn(new ColumnDerived("store_email", "StoreEmail", "Store e-mail", typeof(string)));
            ss.AddColumn(new ColumnDerived("staff_ismanager", "IsManager", "Is manager", typeof(string)));
            ss.AddColumn(new ColumnDirect("manager_id", "ManagerID", "Manager's ID", typeof(string)));
            ss.AddColumn(new ColumnDerived("manager_first_name", "ManagerFirstName", "Manager's first name", typeof(string)));
            ss.AddColumn(new ColumnDerived("manager_last_name", "ManagerLastName", "Manager's last name", typeof(string)));
            ss.AddColumn(new ColumnDirect("passwordhash", "PasswordHash", "", typeof(string)));
            ss.columns[^1].IsRetrievable = false;
            ss.columns[^1].IsVisible = false;
            ss.AddColumn(new ColumnDirect("password", "Password", "", typeof(string)));
            ss.columns[^1].IsRetrievable = false;
            ss.columns[^1].IsVisible = false;

            /* Table Orders */

            Table so = new Table("sales", "orders", typeof(Order));
            so.AddColumn(new ColumnDirect("order_id", "OrderID", "ID", typeof(int)));
            so.AddColumn(new ColumnDirect("customer_id", "CustomerID", "Customer ID", typeof(int)));
            so.AddColumn(new ColumnDerived("customer_first_name", "CustomerFirstName", "Customer first name", typeof(string)));
            so.AddColumn(new ColumnDerived("customer_last_name", "CustomerLastName", "Customer last name", typeof(string)));
            so.AddColumn(new ColumnDerived("customer_email", "CustomerEmail", "Customer e-mail", typeof(string)));
            so.AddColumn(new ColumnDerived("customer_phone", "CustomerPhone", "Customer phone", typeof(string)));
            so.AddColumn(new ColumnDirect("order_status", "OrderStatus", "Order status", typeof(int))); // TODO: could be displayed based on enum
            so.AddColumn(new ColumnDirect("order_date", "OrderDate", "Ordered", typeof(string)));
            so.AddColumn(new ColumnDirect("required_date", "RequiredDate", "Required", typeof(string)));
            so.AddColumn(new ColumnDirect("shipped_date", "ShippedDate", "Shipped", typeof(string)));
            so.AddColumn(new ColumnDirect("store_id", "StoreID", "Store ID", typeof(int)));
            so.AddColumn(new ColumnDerived("store_name", "StoreName", "Store name", typeof(string)));
            so.AddColumn(new ColumnDerived("store_city", "StoreCity", "Store city", typeof(string)));
            so.AddColumn(new ColumnDerived("store_phone", "StorePhone", "Store phone", typeof(string)));
            so.AddColumn(new ColumnDerived("store_email", "StoreEmail", "Store e-mail", typeof(string)));
            so.AddColumn(new ColumnDirect("staff_id", "StaffID", "Staff ID", typeof(int)));
            so.AddColumn(new ColumnDerived("staff_first_name", "StaffFirstName", "Employee first name", typeof(string)));
            so.AddColumn(new ColumnDerived("staff_last_name", "StaffLastName", "Employee last name", typeof(string)));
            so.AddColumn(new ColumnDerived("staff_email", "StaffEmail", "Employee e-mail", typeof(string)));

            /* Table Stocks */

            Table ps = new Table("production", "stocks", typeof(Stock));
            ps.AddColumn(new ColumnDirect("store_id", "StoreID", "Store ID", typeof(int)));
            ps.AddColumn(new ColumnDerived("store_name", "StoreName", "Store name", typeof(string)));
            ps.AddColumn(new ColumnDerived("store_city", "StoreCity", "Store city", typeof(string)));
            ps.AddColumn(new ColumnDerived("store_phone", "StorePhone", "Store phone", typeof(string)));
            ps.AddColumn(new ColumnDerived("store_email", "StoreEmail", "Store e-mail", typeof(string)));
            ps.AddColumn(new ColumnDirect("product_id", "ProductID", "Product ID", typeof(int)));
            ps.AddColumn(new ColumnDerived("product_name", "ProductName", "Product name", typeof(string)));
            ps.AddColumn(new ColumnDerived("product_brand_name", "BrandName", "Brand name", typeof(string)));
            ps.AddColumn(new ColumnDerived("product_category_name", "CategoryName", "Category", typeof(string)));
            ps.AddColumn(new ColumnDirect("quantity", "Quantity", "Quantity", typeof(int)));

            /* Adding all tables to dictionary */

            allTables.Add("Customers", sc);
            allTables.Add("Staff", ss);
            allTables.Add("Orders", so);
            allTables.Add("Stocks", ps);

        }

        /* Table getter */

        public static Table GetTableFromAlias(string alias)
        {
            return allTables[alias];
        }
    }
}
