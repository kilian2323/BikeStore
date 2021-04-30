using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BikeStore.Pages
{
    /// <summary>
    /// Interaction logic for PResultDisplay.xaml
    /// </summary>
    public partial class PResultDisplay : Page, IResultsRequiredBySearchOld, IResultsRequiredByMainOld
    {
        /**
         * Parameters to be changed by the programmer, to match with the tables and their columns.
         */

        private Dictionary<string, List<string>> tableData = new Dictionary<string, List<string>>
        {
            {"sales.customers",
                new List<string> {
                                "Customer ID",
                                "First name",
                                "Last name",
                                "Phone",
                                "E-mail",
                                "Street",
                                "City",
                                "State",
                                "ZIP"
                                 }
            },

            {"sales.order_items",
                new List<string> {
                                "Order ID",
                                "Item ID",
                                "Product ID",
                                "Quantity",
                                "List price",
                                "Discount"
                                }
            },

            {"sales.orders",
                new List<string> {
                                "Order ID",
                                "Customer ID",
                                "Order status",
                                "Order date",
                                "Date required",
                                "Shipping date",
                                "Store ID",
                                "Staff ID"
                                }
            },

            {"sales.staffs",
                new List<string> {
                                "Staff ID",
                                "First name",
                                "Last name",
                                "E-mail",
                                "Phone",
                                "Active",
                                "Store ID",
                                "Manager ID",
                                "Password Hash",
                                "Password"
                                }
            },

            {"sales.stores",
                new List<string> {
                                "Store ID",
                                "Name",
                                "Phone",
                                "E-mail",
                                "Street",
                                "City",
                                "State",
                                "ZIP"
                                }
            },

            {"production.products",
                new List<string> {
                                "Product ID",
                                "Name",
                                "Brand ID",
                                "Category ID",
                                "Model year",
                                "List price"
                                }
            },

            {"production.stocks",
                new List<string> {
                                "Store ID",
                                "Product ID",
                                "Quantity"
                                }
            }
        };

        /* End of definable constants. */

        private List<string> currentTableColumns;
        private DataTable currentData;

        public PResultDisplay()
        {
            InitializeComponent();
        }




        /***********************************************************
         * Required by Search panel (see IResultsRequiredBySearch) *
         ***********************************************************/

        public void SetTablePath(string schemaName, string tableName)
        {
            currentTableColumns = new List<string>(tableData[schemaName+"."+tableName]);
        }
        
        
        /**
         * Returns the names of table columns as they should apear in the combo boxes to the Search panel.
         */
        public ObservableCollection<string> GetColumnNames()
        {
            ObservableCollection<string> obsCollection = new ObservableCollection<string>(currentTableColumns);
            return obsCollection;
        }
        
        /**
         * Takes the query result table from the Search panel and updates the dataGrid to display the results.
         * While doing so, it assigns the dataGrid column bindings according to the strings defining the displayable
         * column names of this table.
         */
        public void SetDataTable(DataTable dt)
        {
            if (currentData == null)
            {                
                int columnNo = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    string binding = column.ColumnName;
                    DataGridTextColumn textColumn = new DataGridTextColumn();
                    textColumn.Header = currentTableColumns[columnNo];
                    columnNo++;
                    textColumn.Binding = new Binding(binding);
                    dataGrid.Columns.Add(textColumn);
                }
            }
            currentData = dt;
            dataGrid.ItemsSource = currentData.DefaultView;
        }


        

        

        

        

        

        

    }
}
