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
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        private int catId;
        private MainWindow mainWnd;

        public NewBook(int _catId, MainWindow _mainWnd)
        {
            InitializeComponent();
            catId = _catId;
            mainWnd = _mainWnd;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Books.AddBook(catId, Title.Text, Author.Text,Publication.Text, Int32.Parse(Pages.Text), Int32.Parse(Year.Text));
            mainWnd.UpdateBooks();
            MessageBox.Show("Book added");
            this.Close();

        }
    }
}
