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

          /*  AutharizationTab.Visibility = Visibility.Collapsed;
            RegistrationTab.Visibility = Visibility.Collapsed;
            CatalogTab.Visibility = Visibility.Visible;
            //SearchTab.Visibility = Visibility.Visible;

            CatalogTab.IsSelected = true;*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterError.Text = "";
            
            if (Password1.Password != Password2.Password)
            {
                RegisterError.Text = "Error: Passwords do not match";
                return;
            }

            if (Login.Text == "")
            {
                RegisterError.Text = "Error: Login is empty";
                return;
            }

            if (Password1.Password == "")
            {
                RegisterError.Text = "Error: Password is empty";
                return;
            }

            Users.CreateUser(Login.Text, Password1.Password);
            MessageBox.Show("Registration completed");

            AutharizationTab.IsSelected = true;
            AuthLogin.Text = Login.Text;
            Login.Text = Password1.Password = Password2.Password = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string login = AuthLogin.Text;
            string password = AuthPassword.Password;
            AuthError.Text = "";

            User user = Users.Auth(login, password);
            if (user == null)
            {
                AuthError.Text = "Incorrect login or password";
                return;
            }

            AutharizationTab.Visibility = Visibility.Collapsed;
            RegistrationTab.Visibility = Visibility.Collapsed;
            CatalogTab.Visibility = Visibility.Visible;
            //SearchTab.Visibility = Visibility.Visible;

            CatalogTab.IsSelected = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NewCategory catWnd = new NewCategory(this);
            catWnd.Activate();
            catWnd.ShowDialog();
        }


        public void UpdateCategoriesList()
        {
            List<BookCategory> categories = Books.GetCategories();
            CategoryList.ItemsSource = categories;
            CategoryList.SelectedIndex = 0;

            UpdateBooksList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCategoriesList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (CategoryList.SelectedIndex == -1)
            {
                MessageBox.Show("No category selected");
                return;
            }

            Books.DeleteCategory(((BookCategory)CategoryList.SelectedItem).Id);
            MessageBox.Show("Category is successfully deleted");

            UpdateCategoriesList();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (CategoryList.SelectedIndex == -1)
                return;
            NewBook bookWnd = new NewBook(((BookCategory)CategoryList.SelectedItem).Id, this);
            bookWnd.Activate();
            bookWnd.Show();
        }

        public void UpdateBooksList()
        {
            if (CategoryList.SelectedIndex == -1)
                return;

            List<Book> books = Books.GetBooks(((BookCategory)CategoryList.SelectedItem).Id, ((ComboBoxItem)SortList.SelectedItem).Content.ToString());
            BooksList.ItemsSource = books;
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooksList();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedIndex == -1)
            {
                MessageBox.Show("No book selected");
                return;
            }

            Books.DeleteBook((Book)BooksList.SelectedItem);
            MessageBox.Show("Book is successfully deleted");
            UpdateBooksList();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedIndex == -1)
            {
                MessageBox.Show("No book selected");
                return;
            }

            NewBook bookWnd = new NewBook(((BookCategory)CategoryList.SelectedItem).Id, this, ((Book)BooksList.SelectedItem).ID);
            bookWnd.Activate();
            bookWnd.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooksList();
        }


    }
}
