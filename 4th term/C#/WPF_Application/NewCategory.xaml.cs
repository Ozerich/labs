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
    /// Interaction logic for NewCategory.xaml
    /// </summary>
    public partial class NewCategory : Window
    {
        private MainWindow mainWnd;

        public NewCategory(MainWindow _mainWnd)
        {
            InitializeComponent();
            mainWnd = _mainWnd;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Books.AddCategory(Title.Text);
                mainWnd.UpdateCategories();
                MessageBox.Show("Category is added");
                this.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
