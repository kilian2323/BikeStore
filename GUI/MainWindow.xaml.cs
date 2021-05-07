using Core.Models.Tables;
using System.Diagnostics;
using System.Windows;
using UI.Interfaces;
using UI.Pages;
using UI.ResultPages;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainRequiredByLogin
    {
        private PSearch searchPanel = null;

        private PLogin loginPage;

        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("MainWindow is constructed");
            loginPage = new PLogin();
            PLogin.OnSuccessfullyLoggedIn -= LoggedIn;
            PLogin.OnSuccessfullyLoggedIn += LoggedIn;
            //ContentFrame.Content = loginPage;
            ContentFrame.Navigate(loginPage);
        }

        /* 
         * Makes controls visible
         * Required due to IMainRequiredByLogin
         */
        public void LoggedIn(Staff staff)
        {
            Debug.WriteLine("OnSuccessfullyLoggedIn invoked");
            Headline.Text = "Logged in as " + staff.FirstName + " " + staff.LastName;
            Bt_Logout.Visibility = Visibility.Visible;
            Controls.Visibility = Visibility.Visible;
            ContentFrame.Content = new PWelcome(staff);
            HeadFrame.Content = new PWelcomeHead();
            HeadFrame.Visibility = Visibility.Visible;
        }

        /* 
         * Hides controls, resets content frame
         * Required due to IMainRequiredByLogin
         */
        public void Logout()
        {
            Headline.Text = "Please log in to continue.";
            Bt_Logout.Visibility = Visibility.Hidden;
            Controls.Visibility = Visibility.Hidden;
            loginPage.Clear();
            ContentFrame.Content = loginPage;
            HeadFrame.Visibility = Visibility.Hidden;
        }

        private void UpdateSearchPanel(IResultsRequiredBySearch resultPage)
        {
            if (searchPanel == null)
            {
                Debug.WriteLine("Creating new Search panel");
                searchPanel = new PSearch(resultPage);
                HeadFrame.Content = searchPanel;
            }
            else
            {
                Debug.WriteLine("Setting Search panel to new Result page");
                searchPanel.Clear();
                searchPanel.SetResultPage(resultPage);
            }
            HeadFrame.Visibility = Visibility.Visible;
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
