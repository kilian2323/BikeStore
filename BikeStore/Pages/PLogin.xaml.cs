using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace BikeStore.Pages
{
    /// <summary>
    /// Interaction logic for PLogin.xaml
    /// </summary>
    /// 


    public partial class PLogin : Page
    {
        private StartWindow parent;
        
        private string connectionString = @"Data Source=.;Initial Catalog=BikeStores;Integrated Security=True;";
        private string commandString = "";
        private SqlDataReader dataReader;
        private SqlCommand command;
        private SqlConnection cnn;

        private int result_staff_id;
        private string result_first_name = "";
        private string result_last_name = "";
        private string result_phone = "";

        public PLogin(StartWindow _parent)
        {
            InitializeComponent();
            parent = _parent;
        }

        private void Query()
        {
            string username = Tb_userName.Text;
            string password = Tb_password.Password;

            commandString = @"USE BikeStores;
                            SELECT
                                   [staff_id]
                                  ,[first_name]
                                  ,[last_name]
                                  ,[email]   
                                  ,[phone]
                                  ,[password]
                            FROM [BikeStores].[sales].[staffs]
                            WHERE [email] = '" + username + "' AND [password] = '" + password + "';";

            cnn = new SqlConnection(connectionString);
            cnn.Open();

            command = new SqlCommand(commandString, cnn);
            dataReader = command.ExecuteReader();

            bool found = true; // TODO: set to false
            while (dataReader.Read() && !found)
            {
                found = true; // we can stop reading now, assuming that there is only one entry with this username/password
                result_staff_id = (int)dataReader.GetValue(0);
                result_first_name = (string)dataReader.GetValue(1);
                result_last_name = (string)dataReader.GetValue(2);
                result_phone = (string)dataReader.GetValue(3);
            }

            if (found)
            {
                //MessageBox.Show("Welcome, " + result_first_name + " " + result_last_name + "!");
                parent.LogIn(result_staff_id, result_first_name, result_last_name, result_phone);
                cnn.Close();
            }
            else
            {
                MessageBox.Show("Username and/or password incorrect!");
                parent.LogOut();
                cnn.Close();
            }
        }

        private void Bt_Enter_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }
    }
}
