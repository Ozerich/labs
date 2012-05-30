using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;


namespace BLL
{
    public static class Books
    {
        public static List<BookCategory> GetCategories()
        {
            return BookCategory.SelectAll();
        }

        public static void AddCategory(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("Name can not be empty");

            BookCategory bookCategory = new BookCategory(name);
            bookCategory.Persist();
        }

        public static void DeleteCategory(int catId)
        {
            BookCategory bookCategory = new BookCategory(catId);
            bookCategory.Delete();
        }

        public static void AddBook(int catId, string Title, string Genre, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat, List<string> Tags)
        {
            Book book = new Book();

            book.Title = Title;
            book.Author = Author;
            book.parentId = catId;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;
            book.Genre = Genre;
            book.Tags = Tags;

            book.Persist();
        }

        public static void UpdateBook(int bookId, int catId, string Title, string Genre, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat, List<string> Tags)
        {
            Book book = GetBook(bookId);

            book.Title = Title;
            book.Author = Author;
            book.parentId = catId;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;
            book.Genre = Genre;
            book.Tags = Tags;

            book.Persist();
        }

        public static List<Book> GetBooks(int parentId, string sort = "Title")
        {
            List<Book> result = Book.SelectAll(parentId);

            if (sort == "Title")
                result = result.OrderBy(x => x.Title).ToList();
            else if (sort == "Author")
                result = result.OrderBy(x => x.Author).ToList();
            else if (sort == "Year")
                result = result.OrderBy(x => x.Year).ToList();
            else if (sort == "Pages number")
                result = result.OrderBy(x => x.PagesCount).ToList();

            return result;
        }

        public static void DeleteBook(Book book)
        {
            book.Delete();
        }

        public static Book GetBook(int bookId)
        {
            return new Book(bookId);
        }

        public static List<Book> Filter(BaseFilter[] filters)
        {
            List<Book> result = new List<Book>();
            List<Book> books = GetBooks(0);

            if (filters.Count() == 0)
                return result;

            foreach (Book book in books)
            {
                bool good = true;
                foreach (BaseFilter filter in filters)
                    if (!filter.Check(book))
                    {
                        good = false;
                        break;
                    }

                if (good)
                    result.Add(book);
            }

            return result;
        }

        public static List<string> GetAllTags()
        {
            List<string> result = new List<string>();

            List<Book> books = GetBooks(0);

            foreach (Book book in books)
                foreach (string tag in book.Tags)
                    if (result.Contains(tag) == false)
                        result.Add(tag);

            return result;
        }
    }
}
