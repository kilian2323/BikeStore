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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BikeStore.Pages
{
    /// <summary>
    /// Interaction logic for PWelcome.xaml
    /// </summary>
    /// 
    
    public partial class PWelcome : Page
    {
        private StartWindow parent;

        public PWelcome(StartWindow _parent)
        {
            InitializeComponent();
            parent = _parent;
            Tb_phone.Text = parent.PhoneNumber;
            Tb_userName.Text = parent.UserName;
        }

        private void Bt_EditMyProfile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
