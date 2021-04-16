using System;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for EditRow.xaml
    /// </summary>
    public partial class EditRow : Window
    {
        private string connectionString = @"Data Source=.;Initial Catalog=BikeStores;Integrated Security=True;";
        private string commandString = "";
        private SqlCommand command;
        private SqlConnection cnn;

        private int staff_id;

        private string new_first_name = "";
        private string new_last_name = "";
        private string new_email = "";
        
        private string old_first_name = "";
        private string old_last_name = "";
        private string old_email = "";

        public EditRow(string _staff_id, string _old_first_name, string _old_last_name, string _old_email)
        {
            InitializeComponent();
            staff_id = Int16.Parse(_staff_id);
            old_first_name = _old_first_name;
            old_last_name = _old_last_name;
            old_email = _old_email;
            tb_firstName.Text = old_first_name;
            tb_lastName.Text = old_last_name;
            tb_email.Text = old_email;
            System.Diagnostics.Debug.WriteLine("Old first name: " + old_first_name);
            System.Diagnostics.Debug.WriteLine("Old last name : " + old_last_name);
            System.Diagnostics.Debug.WriteLine("Old email     : " + old_email);
        }

        private void Query()
        {   new_first_name = tb_firstName.Text;
            new_last_name = tb_lastName.Text;
            new_email = tb_email.Text;

            System.Diagnostics.Debug.WriteLine("Updating table...");
            System.Diagnostics.Debug.WriteLine("Staff ID      : " + staff_id);
            System.Diagnostics.Debug.WriteLine("New first name: " + new_first_name);
            System.Diagnostics.Debug.WriteLine("New last name : " + new_last_name);
            System.Diagnostics.Debug.WriteLine("New email     : " + new_email);
                        
            commandString = @"UPDATE [sales].[staffs]
                              SET
                                   [first_name] = '" + new_first_name + "', [last_name] = '" + new_last_name + "', [email] = '" + new_email
                                 + "' WHERE [staff_id] = '" + staff_id + "';";
            
            
            System.Diagnostics.Debug.WriteLine(commandString);

            cnn = new SqlConnection(connectionString);
            cnn.Open();

            command = new SqlCommand(commandString, cnn);
            command.ExecuteNonQuery();

            cnn.Close();
        }

        private void Bt_Change_Click(object sender, RoutedEventArgs e)
        {
            new_first_name = tb_firstName.Text;
            new_last_name = tb_lastName.Text;
            new_email = tb_email.Text;

            System.Diagnostics.Debug.WriteLine("Old first name: "+old_first_name);
            System.Diagnostics.Debug.WriteLine("Old last name : " + old_last_name);
            System.Diagnostics.Debug.WriteLine("Old email     : " + old_email);

            ArrayList arr = new ArrayList();
            arr.Add(new_first_name);
            arr.Add(new_last_name);
            arr.Add(new_email);

            Query();

            //MainWindow.self.ReloadData(arr);
            Close();
        }

        private void Bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
