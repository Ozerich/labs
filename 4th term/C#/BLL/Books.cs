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

        public static void AddBook(int catId, string Title, string Author, string Publication, int PagesCount, int Year)
        {
            Book book = new Book();

            book.Title = Title;
            book.Author = Author;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;

            BookDal.CreateBook(catId, book);
        }

        public static List<Book> GetBooks(int parentId)
        {
            return BookDal.GetBooks(parentId);
        }

        public static void DeleteBook(Book book)
        {
            BookDal.DeleteBook(book.ID);
        }
    }
}
