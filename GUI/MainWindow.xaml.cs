using Core.Definitions;
using Core.Models;
using System.Diagnostics;
using System.Windows;
using UI.Interfaces;
using UI.Pages;
using UI.ResultPages;
using System;
using BLL.Tables;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainRequiredByLogin
    {
        private PSearch searchPanel = null;

        private PLogin loginPage;
        private BLL_Staffs bLLStaffs;
        private BLL_Customers bllCustomers;

        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("MainWindow is constructed");
            loginPage = new PLogin();
            bLLStaffs = new BLL_Staffs();
            bllCustomers = new BLL_Customers();
            PLogin.OnSuccessfullyLoggedIn -= Login;
            PLogin.OnSuccessfullyLoggedIn += Login;
            BLL_Staffs.OnBLLStaffViolation -= BLLStaffViolated;
            BLL_Staffs.OnBLLStaffViolation += BLLStaffViolated;
            Tables.GenerateTables();
            ContentFrame.Content = loginPage;
        }

        

        public void BLLStaffViolated(string error)
        {
            Debug.WriteLine("BLLStaffViolated invoked");
            MessageBox.Show(error);
        }

        /* 
         * Makes controls visible
         * Required due to IMainRequiredByLogin
         */
        public void Login(Staff staff)
        {
            Debug.WriteLine("OnSuccessfullyLoggedIn invoked");
            Headline.Text = "Logged in as " + staff.FirstName + " " + staff.LastName;
            Bt_Logout.Visibility = Visibility.Visible;
            Controls.Visibility = Visibility.Visible;
            ContentFrame.Content = new PWelcome(staff);
        }

        /* 
         * Hides controls
         * Required due to IMainRequiredByLogin
         */
        public void Logout()
        {
            Headline.Text = "Please log in to continue.";
            Bt_Logout.Visibility = Visibility.Hidden;
            Controls.Visibility = Visibility.Hidden;
            loginPage.Clear();
            ContentFrame.Content = loginPage;
            SearchFrame.Visibility = Visibility.Hidden;
        }

        private void UpdateSearchPanel(IResultsRequiredBySearch resultPage)
        {
            if (searchPanel == null)
            {
                System.Diagnostics.Debug.WriteLine("Creating new Search panel");
                searchPanel = new PSearch(resultPage);
                SearchFrame.Content = searchPanel;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Setting Search panel to new Result page");
                searchPanel.Clear();
                searchPanel.SetResultPage(resultPage);
            }
            SearchFrame.Visibility = Visibility.Visible;
        }

        private void Bt_Customers_Click(object sender, RoutedEventArgs e)
        {
            IResultsRequiredBySearch resultsPage = new PCustomers();
            ContentFrame.Content = resultsPage; // display Customers page
            UpdateSearchPanel(resultsPage);
            searchPanel.DoSearch();
        }

        private void Bt_Staff_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bt_Orders_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bt_Products_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bt_Stock_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bt_Logout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
    }
}
