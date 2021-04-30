using BLL.Tables;
using Core.Classes;
using Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using UI.Interfaces;

namespace UI.ResultPages
{
    /// <summary>
    /// Interaction logic for PCustomers.xaml
    /// </summary>
    public partial class PCustomers : Page, IResultsRequiredBySearch
    {
        private static string tableAlias = "Customers";
        private ObservableCollection<Customer> customerList { get; set; }
        private BLL_Customers bLL_customers = new BLL_Customers();

        public PCustomers()
        {
            InitializeComponent();
            customerList = new ObservableCollection<Customer>();
            dataGrid.ItemsSource = customerList; // create binding
        }

        /*
         * Return the tableAlias
         * Required due to IResultsRequiredBySearch
         */
        public string GetTableAlias()
        {
            return tableAlias;
        }

        /*
         * Perform search and update StaffList
         * Required due to IResultsRequiredBySearch
         */
        public void DoSearch(Search search)
        {
            customerList.Clear();
            bLL_customers.GetRows(search).ForEach(customerList.Add);
        }
    }
}
