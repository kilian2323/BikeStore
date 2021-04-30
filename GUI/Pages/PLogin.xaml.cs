using BLL.Tables;
using Core.Classes;
using Core.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UI.Interfaces;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for PLogin.xaml
    /// </summary>

    public partial class PLogin : Page, IResultsRequiredBySearch
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
            BLL_Staffs bs = new BLL_Staffs();
            Staff loggedIn = bs.DoLogin(Tb_userName.Text, Tb_password.Password);
            if (loggedIn != null)
            {
                OnSuccessfullyLoggedIn.Invoke(loggedIn);
            }            
        }

        public string GetTableAlias()
        {
            throw new System.NotImplementedException();
        }

        public void DoSearch(Search search)
        {
            throw new System.NotImplementedException();
        }
    }
}
