using BLL.Tables;
using Core.Models.Tables;
using System.Windows;
using System.Windows.Controls;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for PLogin.xaml
    /// </summary>

    public partial class PLogin : Page
    {
        public delegate void SuccessfullyLoggedIn(Staff staff);
        public static event SuccessfullyLoggedIn OnSuccessfullyLoggedIn;
        

        public PLogin()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            Tb_userName.Text = "";
            Tb_password.Password = "";
        }

        private void Bt_Enter_Click(object sender, RoutedEventArgs e)
        {
            var bs = new BLL_Staffs();
            /* Check here only for null or empty, then pass the strings
             * on to the DLL and assemble the query there
             */
            if (string.IsNullOrEmpty(Tb_userName.Text) || string.IsNullOrEmpty(Tb_password.Password))
            {
                MessageBox.Show("Username and password must not be empty!");
                return;
            }
            Staff loggedIn = bs.DoLogin(Tb_userName.Text, Tb_password.Password);
            if (loggedIn != null)
            {
                OnSuccessfullyLoggedIn?.Invoke(loggedIn);
            }
            else
            {
                MessageBox.Show("Username / password wrong, user not found or insufficient privileges.");
            }
        }      
    }
}
