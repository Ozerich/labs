using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Entities;


namespace BLL
{
    public static class Books
    {
        public static List<BookCategory> GetCategories()
        {
            Catalog catalog = new Catalog();

            var q = from c in catalog.BookCategories select c;

            return q.ToList<BookCategory>();
        }

        public static void AddCategory(string name)
        {
            Catalog catalog = new Catalog();

            BookCategory cat = new BookCategory();
            cat.Name = name;

            catalog.BookCategories.InsertOnSubmit(cat);
            catalog.SubmitChanges();
        }

        public static void DeleteCategory(int catId)
        {
            Catalog catalog = new Catalog();

            var categoryQuery = from category in catalog.BookCategories where category.Id == catId select category;

            catalog.BookCategories.DeleteOnSubmit(categoryQuery.First());
            catalog.SubmitChanges();
        }

        public static void AddBook(int catId, string Title, string Genre, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat, List<string> Tags)
        {
            
            Book book = new Book();

            book.parentId = catId;
            book.Title = Title;
            book.Author = Author;
            book.parentId = catId;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;
            book.Genre = Genre;
            book.Tags = Tags;

            Catalog catalog = new Catalog();

            catalog.Books.InsertOnSubmit(book);

            catalog.SubmitChanges();

            int bookId = book.Id;

            foreach (string tag in book.Tags)
            {
                BookTag bookTag = new BookTag();
                bookTag.Name = tag;
                bookTag.Book_Id = bookId;

                catalog.Tags.InsertOnSubmit(bookTag);
            }

            catalog.SubmitChanges();

        }

        public static void UpdateBook(int bookId, int catId, string Title, string Genre, string Author, string Publication, int PagesCount, int Year, EnumFileFormat fileFormat, List<string> Tags)
        {
            Catalog catalog = new Catalog();

            var q = from ord in catalog.Books where ord.Id == bookId select ord;

            Book book = q.First();
            book.Title = Title;
            book.Author = Author;
            book.parentId = catId;
            book.Publication = Publication;
            book.PagesCount = PagesCount;
            book.Year = Year;
            book.FileFormat = fileFormat;
            book.Genre = Genre;
            book.Tags = Tags;

            var tagQuery = from tag in catalog.Tags where tag.Book_Id == bookId select tag;
            foreach (BookTag bookTag in tagQuery)
            {

                catalog.Tags.DeleteOnSubmit(bookTag);
            }

            foreach(string tag in book.Tags)
            {
                BookTag bookTag = new BookTag();
                bookTag.Name = tag;
                bookTag.Book_Id = bookId;

                catalog.Tags.InsertOnSubmit(bookTag);
            }

            catalog.SubmitChanges();
        }

        public static List<Book> GetBooks(int parentId, string sort = "Title")
        {
            List<Book> result = new List<Book>();

            Catalog catalog = new Catalog();

            var q = parentId != 0 ? from book in catalog.Books where book.parentId == parentId select book : from book in catalog.Books select book;
            foreach (Book book in q)
                result.Add(book);

            if (sort == "Title")
                result = result.OrderBy(x => x.Title).ToList();
            else if (sort == "Author")
                result = result.OrderBy(x => x.Author).ToList();
            else if (sort == "Year")
                result = result.OrderBy(x => x.Year).ToList();
            else if (sort == "Pages number")
                result = result.OrderBy(x => x.PagesCount).ToList();

            foreach (Book book in result)
            {
                book.Tags = new List<string>();

                var qTag = from tag in catalog.Tags where tag.Book_Id == book.Id select tag;
                foreach (BookTag bookTag in qTag)
                    book.Tags.Add(bookTag.Name);
            }
         
            return result;
        }

        public static void DeleteBook(Book book)
        {
            Catalog catalog = new Catalog();

            var tagq = from tag in catalog.Tags where tag.Book_Id == book.Id select tag;
            foreach (BookTag tag in tagq)
                catalog.Tags.DeleteOnSubmit(tag);

            var q = from qBook in catalog.Books where qBook.Id == book.Id select book;

            Book deleteBook = q.First();

            catalog.Books.Attach(deleteBook);
            catalog.Books.DeleteOnSubmit(deleteBook);

            catalog.SubmitChanges();
        }

        public static Book GetBook(int bookId)
        {
            Catalog catalog = new Catalog();

            var q = from book in catalog.Books where book.Id == bookId select book;
            Book resultBook = q.First();

            resultBook.Tags = new List<string>();
            var tagQuery = from tag in catalog.Tags where tag.Book_Id == bookId select tag;
            foreach (BookTag tag in tagQuery)
                resultBook.Tags.Add(tag.Name);

            return resultBook;
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
