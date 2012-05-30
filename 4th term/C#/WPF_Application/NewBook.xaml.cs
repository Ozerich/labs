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
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        private int catId, bookId;
        private MainWindow mainWnd;

        public NewBook(int _catId, MainWindow _mainWnd, int _bookId = 0)
        {
            InitializeComponent();
            catId = _catId;
            mainWnd = _mainWnd;
            bookId = _bookId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<BookCategory> categories = Books.GetCategories();

            CategoriesList.ItemsSource = categories;
            CategoriesList.SelectedIndex = 0;

            BookFormat.ItemsSource = Enum.GetValues(typeof(EnumFileFormat));
            BookFormat.SelectedIndex = 0;

            if (bookId != 0)
            {
                Book book = new Book(bookId);

                BookTitle.Text = book.Title;
                BookAuthor.Text = book.Author;
                BookPublisher.Text = book.Publication;
                BookPages.Text = book.PagesCount.ToString();
                BookYear.Text = book.Year.ToString();
                BookGenre.Text = book.Genre.ToString();

                foreach (string bookName in book.Tags)
                    TagList.Items.Add(bookName);

                BookFormat.SelectedItem = book.FileFormat;

                NewBookBtn.Visibility = Visibility.Hidden;
                SaveBookBtn.Visibility = Visibility.Visible;

                this.Title = "Edit Book";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<string> tags = new List<string>();
            foreach (string tag in TagList.Items)
                tags.Add(tag.ToString());
            
            try
            {
                Books.AddBook(((BookCategory)CategoriesList.SelectedItem).Id, BookTitle.Text, BookGenre.Text, BookAuthor.Text, BookPublisher.Text, Int32.Parse(BookPages.Text), Int32.Parse(BookYear.Text), (EnumFileFormat)BookFormat.SelectedItem, tags);
                mainWnd.UpdateBooksList();
                MessageBox.Show("Book is successfully added");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> tags = new List<string>();
            foreach(string tag in TagList.Items)
                tags.Add(tag.ToString());

            try
            {
                Books.UpdateBook(bookId, ((BookCategory)CategoriesList.SelectedItem).Id, BookTitle.Text, BookGenre.Text, BookAuthor.Text, BookPublisher.Text, Int32.Parse(BookPages.Text), Int32.Parse(BookYear.Text), (EnumFileFormat)BookFormat.SelectedItem, tags);
                mainWnd.UpdateBooksList();
                MessageBox.Show("Book is successfully updated");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void AddTagBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewTag.Text == "")
            {
                MessageBox.Show("Tag is empty");
                return;
            }

            foreach (string tag in TagList.Items)
                if (tag == NewTag.Text)
                {

                    NewTag.Text = "";
                    return;
                }

            TagList.Items.Add(NewTag.Text);
            NewTag.Text = "";
        }
    }
}
