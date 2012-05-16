using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;


namespace BLL
{
    public static class Books
    {
        public static List<BookCategory> GetCategories()
        {
            return BookDal.GetCategories();
        }

        public static void AddCategory(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("Name can not be empty");
            BookDal.CreateCategory(name);
        }

        public static void DeleteCategory(int catId)
        {
            BookDal.DeleteCategory(catId);
        }

        public static void AddBook(int catId, string Title, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat)
        {
            Book book = new Book();

            book.Title = Title;
            book.Author = Author;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;

            BookDal.CreateBook(catId, book);
        }

        public static void UpdateBook(int bookId, int catId, string Title, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat)
        {
            Book book = GetBook(bookId);

            book.Title = Title;
            book.Author = Author;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;

            BookDal.UpdateBook(book);
        }

        public static List<Book> GetBooks(int parentId, string sort)
        {
            List<Book> result = BookDal.GetBooks(parentId);

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
            BookDal.DeleteBook(book.ID);
        }

        public static Book GetBook(int bookId)
        {
            return BookDal.GetBook(bookId);
        }
    }
}
