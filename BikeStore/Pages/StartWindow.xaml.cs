using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;




namespace BikeStore.Pages
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        // Info about the user who is logged in
        private bool isLoggedIn = false;
        private int staffId = -1;
        private string userName = "";
        private string phoneNumber = "";
        private PFilterPanel searchPanel = null;
        
        public StartWindow()
        {
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            ContentFrame.Content = new PLoginOld(this);            
        }

        public void LogIn(int _staff_id, string _first_name, string _last_name, string _phone)
        {
            IsLoggedIn = true;
            staffId = _staff_id;
            phoneNumber = _phone;
            Headline.Text = "Logged in as " + _first_name + " " + _last_name;
            Bt_Logout.Visibility = Visibility.Visible;
            Controls.Visibility = Visibility.Visible;
            ContentFrame.Content = new PWelcomeOld(this);
        }

        public void LogOut()
        {
            IsLoggedIn = false;
            staffId = -1;
            Headline.Text = "Please log in to continue.";
            Bt_Logout.Visibility = Visibility.Hidden;
            Controls.Visibility = Visibility.Hidden;
            ContentFrame.Content = new PLoginOld(this);
            SearchFrame.Visibility = Visibility.Hidden;
        }


        public bool IsLoggedIn { get => isLoggedIn; set => isLoggedIn = value; }
        public int StaffId { get => staffId; set => staffId = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        private void updateSearchPanel(IResultsRequiredBySearchOld page, string schemaName, string tableName)
        {
            if (searchPanel == null)
            {
                System.Diagnostics.Debug.WriteLine("Creating new Search panel");
                searchPanel = new PFilterPanel();
                SearchFrame.Content = searchPanel;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Clearing Search panel");
                searchPanel.Clear();
            }
            SearchFrame.Visibility = Visibility.Visible;
            System.Diagnostics.Debug.WriteLine("Setting results page in Search Panel");
            searchPanel.SetResultsPage(page, schemaName, tableName);
        }
        private void Bt_Customers_Click(object sender, RoutedEventArgs e)
        {
            PResultDisplay resultsPage = new PResultDisplay();
            ContentFrame.Content = resultsPage;
            updateSearchPanel(resultsPage,"sales","customers");            
        }

        private void Bt_Staff_Click(object sender, RoutedEventArgs e)
        {
            PResultDisplay resultsPage = new PResultDisplay();
            ContentFrame.Content = resultsPage;
            updateSearchPanel(resultsPage, "sales", "staffs");
        }

        private void Bt_Orders_Click(object sender, RoutedEventArgs e)
        {
            PResultDisplay resultsPage = new PResultDisplay();
            ContentFrame.Content = resultsPage;
            updateSearchPanel(resultsPage, "sales", "orders");
        }

        private void Bt_Products_Click(object sender, RoutedEventArgs e)
        {
            PResultDisplay resultsPage = new PResultDisplay();
            ContentFrame.Content = resultsPage;
            updateSearchPanel(resultsPage, "production", "products");
        }

        private void Bt_Stock_Click(object sender, RoutedEventArgs e)
        {
            PResultDisplay resultsPage = new PResultDisplay();
            ContentFrame.Content = resultsPage;
            updateSearchPanel(resultsPage, "production", "stocks");
        }

        private void Bt_Logout_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }


    }
}
