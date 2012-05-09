using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;

namespace WPF_Application
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password1.Password;

            Error.Text = "";

            if (Users.CheckExist(login))
            {
                Error.Text = "User with same Login exist";
                return;
            }

            if (password.Length == 0)
            {
                Error.Text = "Password is empty";
                return;
            }

            if (password != Password2.Password)
            {
                Error.Text = "Password is not equal";
                return;
            }

            Users.CreateUser(login, password);
            MessageBox.Show("Registration completed");
            this.Hide();
        }
    }
}
