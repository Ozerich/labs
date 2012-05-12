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
            if (CategoryList.SelectedValue != null)
            {
                NewBook newBookWnd = new NewBook(Int32.Parse(CategoryList.SelectedValue.ToString()), this);
                newBookWnd.Activate();
                newBookWnd.ShowDialog();
            }
            else
                MessageBox.Show("Category is not selected");
        }

        public void UpdateCategories()
        {
            CategoryList.Items.Clear();
            List<BookCategory> categories = Books.GetCategories();
            foreach (BookCategory cat in categories)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = cat.Name;
                item.DataContext = cat.Id;
                CategoryList.Items.Add(item);
            }
            CategoryList.SelectedIndex = 0;
            UpdateBooks();
        }

        public void UpdateBooks()
        {
            int catId = Int32.Parse(CategoryList.SelectedValue.ToString());
            List<Book> books = Books.GetBooks(catId);
            BookList.ItemsSource = books;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewCategory NewCategoryWnd = new NewCategory(this);
            NewCategoryWnd.Activate();
            NewCategoryWnd.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCategories();
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            int catId = Int32.Parse(CategoryList.SelectedValue.ToString());
            Books.DeleteCategory(catId);
            UpdateCategories();
            MessageBox.Show("Deleted");
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (BookList.SelectedIndex == -1)
            {
                MessageBox.Show("No book selected");
                return;
            }

            Book book = (Book) BookList.SelectedItem;
            Books.DeleteBook(book);
            MessageBox.Show("Deleted");
            UpdateBooks();
        }

    }
}
