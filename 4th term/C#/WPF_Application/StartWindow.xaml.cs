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
using Entities;

namespace WPF_Application
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Activate();
            reg.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            Error.Text = "";

            User user = Users.Auth(login, password);
            if (user == null)
            {
                Error.Text = "Incorrect login or password";
                return;
            }

            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Activate();
            mainWindow.Show();

            this.Hide();
        }
    }
}
