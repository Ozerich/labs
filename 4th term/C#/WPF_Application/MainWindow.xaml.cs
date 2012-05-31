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

            if (Users.CurrentUser.IsAdmin)
                NewBookBtn.Visibility = EditBookBtn.Visibility = DeleteBookBtn.Visibility = NewCategorybtn.Visibility = 
                    DeleteCategoryBtn.Visibility = DeleteBookSearchBtn.Visibility = EditBookSearchBtn.Visibility = Visibility.Visible;
     

            AutharizationTab.Visibility = Visibility.Collapsed;
            RegistrationTab.Visibility = Visibility.Collapsed;
            CatalogTab.Visibility = Visibility.Visible;
            SearchTab.Visibility = Visibility.Visible;

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

            NewBookBtn.Visibility = EditBookBtn.Visibility = DeleteBookBtn.Visibility = 
                PutBookBtn.Visibility = TakeBookBtn.Visibility = NewCategorybtn.Visibility = DeleteCategoryBtn.Visibility = 
                    DeleteBookSearchBtn.Visibility = EditBookSearchBtn.Visibility = Visibility.Collapsed;
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

            List<string> tags = Books.GetAllTags();
            TagListFilter.ItemsSource = tags;
            TagListFilter.SelectedIndex = 0;
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

            NewBook bookWnd = new NewBook(((BookCategory)CategoryList.SelectedItem).Id, this, ((Book)BooksList.SelectedItem).Id);
            bookWnd.Activate();
            bookWnd.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBooksList();
        }

        private void SearchSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<BaseFilter> filters = new List<BaseFilter>();

            if (TitleFilterEnabled.IsChecked == true)
            {
                TitleFilter filter = new TitleFilter() { Options = TitleFilter.Text };
                filters.Add(filter);
            }

            if (AuthorFilterEnabled.IsChecked == true)
            {
                AuthorFilter filter = new AuthorFilter() { Options = AuthorFilter.Text };
                filters.Add(filter);
            }

            if (PublisherTitleEnabled.IsChecked == true)
            {
                PublicationFilter filter = new PublicationFilter() { Options = PublisherFilter.Text };
                filters.Add(filter);
            }

            if (FileFormatFilterEnabled.IsChecked == true)
            {
                FileFormatFilter filter = new FileFormatFilter() { Options = FileFormatFilter.SelectedItem};
                filters.Add(filter);
            }

            if (YearFilterEnabled.IsChecked == true)
            {
                Dictionary<string, int> options = new Dictionary<string,int>();

                try
                {
                    options["min"] = Int32.Parse(YearFilterStart.Text);
                    options["max"] = Int32.Parse(YearFilterFinish.Text);

                    YearFilter filter = new YearFilter() { Options = options };
                    filters.Add(filter);
                }
                catch
                {
                    MessageBox.Show("Range is incorrect");
                }
            }

            if (PagesFilterEnabled.IsChecked == true)
            {
                Dictionary<string, int> options = new Dictionary<string,int>();

                try
                {
                    options["min"] = Int32.Parse(PagesFilterStart.Text);
                    options["max"] = Int32.Parse(PagesFilterFinish.Text);

                    PagesFilter filter = new PagesFilter() { Options = options };
                    filters.Add(filter);
                }
                catch
                {
                    MessageBox.Show("Range is incorrect");
                }

            }

            if (TagFilterEnabled.IsChecked == true && TagListFilter.SelectedIndex != -1)
            {
                TagFilter tagFilter = new TagFilter() { Options = TagListFilter.SelectedItem.ToString() };
                filters.Add(tagFilter);
            }

            List<Book> books = Books.Filter(filters.ToArray());
            FilterResults.ItemsSource = books;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            FileFormatFilter.ItemsSource = Enum.GetValues(typeof(EnumFileFormat));
            FileFormatFilter.SelectedIndex = 0;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (FilterResults.SelectedIndex == -1)
            {
                MessageBox.Show("No book selected");
                return;
            }

            NewBook bookWnd = new NewBook(0, this, ((Book)FilterResults.SelectedItem).Id);
            bookWnd.Activate();
            bookWnd.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (FilterResults.SelectedIndex == -1)
            {
                MessageBox.Show("No book selected");
                return;
            }

            Books.DeleteBook((Book)FilterResults.SelectedItem);
            MessageBox.Show("Book is successfully deleted");
            UpdateBooksList();


        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book book = (Book)BooksList.SelectedItem;

            if (book == null)
                return;

            PutBookBtn.Visibility = TakeBookBtn.Visibility = Visibility.Collapsed;

            if (Users.CurrentUser.HasBook(book.Id))
                PutBookBtn.Visibility = Visibility.Visible;
            else
                TakeBookBtn.Visibility = Visibility.Visible;
        }

        private void TakeBookBtn_Click(object sender, RoutedEventArgs e)
        {
            Book book = BooksList.SelectedItem as Book;
            UserBook.TakeBook(book.Id, Users.CurrentUser.Id);

            TakeBookBtn.Visibility = Visibility.Collapsed;
            PutBookBtn.Visibility = Visibility.Visible;
        }

        private void PutBookBtn_Click(object sender, RoutedEventArgs e)
        {
            Book book = BooksList.SelectedItem as Book;
            UserBook.PutBook(book.Id, Users.CurrentUser.Id);

            TakeBookBtn.Visibility = Visibility.Visible;
            PutBookBtn.Visibility = Visibility.Collapsed;
        }

    }
}
