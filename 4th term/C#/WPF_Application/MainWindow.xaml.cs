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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using Entities;

namespace WPF_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User user)
            : this()
        {
            this.user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBookWnd = new NewBook();
            newBookWnd.Activate();
            newBookWnd.ShowDialog();
        }

        private void UpdateCategories()
        {
            List<BookCategory> categories = Books.GetCategories();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCategories();
        }
    }
}
