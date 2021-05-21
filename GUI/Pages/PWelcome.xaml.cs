using Core.Models.Tables;
using System.Windows;
using System.Windows.Controls;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for PWelcome.xaml
    /// </summary>
    public partial class PWelcome : Page
    {
        public PWelcome(Staff e)
        {
            InitializeComponent();
            Tbl_fullName.Text = e.FirstName + " " + e.LastName;
            Tbl_phone.Text = e.Phone;
            Tbl_userName.Text = e.Email;
        }

        public void Bt_EditMyProfile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
