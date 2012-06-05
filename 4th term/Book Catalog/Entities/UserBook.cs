using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Entities
{
    [Table(Name = "UserBooks")]
    public class UserBook
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int Book_Id { get; set; }

        [Column]
        public int User_Id { get; set; }

        public static void TakeBook(int bookId, int userId)
        {
            Catalog catalog = new Catalog();

            UserBook userBook = new UserBook();
            userBook.Book_Id = bookId;
            userBook.User_Id = userId;

            catalog.UserBooks.InsertOnSubmit(userBook);
            catalog.SubmitChanges();
        }

        public static void PutBook(int bookId, int userId)
        {
            Catalog catalog = new Catalog();

            var q = from book in catalog.UserBooks where book.Book_Id == bookId where book.User_Id == userId select book;

            UserBook userBook = q.First();
            catalog.UserBooks.DeleteOnSubmit(userBook);
            catalog.SubmitChanges();
        }
    }
}
